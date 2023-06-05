import { Component } from '@angular/core';
import { distinctUntilChanged, Subject, takeUntil } from "rxjs";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { AppState } from "../../../store/app.reducer";
import { ActivatedRoute, Router } from "@angular/router";
import { selectCurrentGroupId } from "../../../groups/group-hub/store/group-hub.selectors";
import { filter, take } from "rxjs/operators";
import { startGame } from "../store/text-plot-game.actions";
import { selectCreatedGame } from "../store/text-plot-game.selectors";

@Component({
  selector: 'app-create-text-plot-game',
  templateUrl: './create-text-plot-game.component.html',
  styleUrls: ['./create-text-plot-game.component.scss']
})
export class CreateTextPlotGameComponent {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  createTexPlotGameForm: FormGroup;

  constructor(private fb: FormBuilder, private store: Store<AppState>, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.createTexPlotGameForm = this.fb.group({
      topic: ['', Validators.required],
    });
  }

  onSubmit() {
    if (!this.createTexPlotGameForm.valid) {
      return;
    }

    const value = this.createTexPlotGameForm.value;
    const topic = value.topic;

    this.store.select(selectCurrentGroupId).pipe(filter(id => !!id), take(1))
      .subscribe(groupId => {
        this.store.dispatch(startGame({ groupId, game: { topic } }));
      });

    this.store.select(selectCreatedGame)
      .pipe(distinctUntilChanged(), filter(id => !!id), takeUntil(this.destroyed$), take(1))
      .subscribe(_ => {
        this.router.navigate(['../'], { relativeTo: this.route });
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
