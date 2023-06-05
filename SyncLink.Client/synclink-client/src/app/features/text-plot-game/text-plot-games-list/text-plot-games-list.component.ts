import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppState } from "../../../store/app.reducer";
import { Store } from "@ngrx/store";
import { ActivatedRoute, Router } from "@angular/router";
import { distinctUntilChanged, Observable, Subject, takeUntil } from "rxjs";
import { filter, map, switchMap, tap } from "rxjs/operators";
import * as TextPlotGameActions from "../store/text-plot-game.actions";
import { TextPlotGame } from "../store/text-plot-game.reducer";
import { selectGamesByGroupId } from "../store/text-plot-game.selectors";

@Component({
  selector: 'app-text-plot-games-list',
  templateUrl: './text-plot-games-list.component.html',
  styleUrls: ['./text-plot-games-list.component.scss']
})
export class TextPlotGamesListComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  games$: Observable<TextPlotGame[]>;

  constructor(private store: Store<AppState>,
              private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  public ngOnInit() {
    this.games$ = this.activatedRoute.paramMap.pipe(
      takeUntil(this.destroyed$),
      map((p) => +p.get('groupId')),
      filter(id => !!id),
      distinctUntilChanged(),
      tap((id) => this.store.dispatch(TextPlotGameActions.getGames({groupId: id}))),
      switchMap((id) => this.store.select(selectGamesByGroupId(id)))
    );
  }

  navigateToTextPlotGame(id: number) {
    this.router.navigate([id], { relativeTo: this.activatedRoute });
  }

  openCreateForm() {
    this.router.navigate(['create'], { relativeTo: this.activatedRoute });
  }

  public ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
