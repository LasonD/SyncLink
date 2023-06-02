import { Component, OnDestroy, OnInit } from '@angular/core';
import { GroupsState } from "../store/groups.reducer";
import { Store } from "@ngrx/store";
import { closeGroup, getGroup, openGroup } from "./store/group-hub.actions";
import { ActivatedRoute, Router } from "@angular/router";
import { distinctUntilChanged, Observable, Subject, takeUntil } from "rxjs";
import { filter, map } from "rxjs/operators";
import { selectGroupHubGroup, selectGroupHubLoading } from "./store/group-hub.selectors";
import { Group } from "../../models/group.model";

@Component({
  selector: 'app-group-hub',
  templateUrl: './group-hub.component.html',
  styleUrls: ['./group-hub.component.scss']
})
export class GroupHubComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  isLoading$: Observable<boolean>;
  group: Group;

  constructor(private store: Store<GroupsState>,
              private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  ngOnInit() {
    this.isLoading$ = this.store.select(selectGroupHubLoading).pipe(takeUntil(this.destroyed$));

    this.store.select(selectGroupHubGroup)
      .pipe(
        takeUntil(this.destroyed$)
      ).subscribe((g) => {
        this.group = g;
        this.store.dispatch(openGroup({ groupId: this.group?.id }));
    })

    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        map((p) => +p.get('groupId')),
        filter(id => !!id),
        distinctUntilChanged()
      ).subscribe((id) => {
        this.store.dispatch(getGroup({ id }))
    });
  }

  ngOnDestroy() {
    this.store.dispatch(closeGroup({ groupId: this.group?.id }));
    this.destroyed$.next(true);
  }

  navigateToHub() {
    this.router.navigate(['groups', this.group.id, 'hub']);
  }
}
