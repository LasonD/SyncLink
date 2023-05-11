import { Component, OnDestroy, OnInit } from '@angular/core';
import { GroupsState } from "../store/groups.reducer";
import { Store } from "@ngrx/store";
import { getGroup } from "./store/group-hub.actions";
import { ActivatedRoute } from "@angular/router";
import { Subject, takeUntil } from "rxjs";
import { map } from "rxjs/operators";
import { selectGroupHubGroup } from "./store/group-hub.selectors";
import { GroupOverview } from "../models/group.model";

@Component({
  selector: 'app-group-hub',
  templateUrl: './group-hub.component.html',
  styleUrls: ['./group-hub.component.scss']
})
export class GroupHubComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  group: GroupOverview;

  constructor(private store: Store<GroupsState>,
              private activatedRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.store.select(selectGroupHubGroup)
      .pipe(
        takeUntil(this.destroyed$)
      ).subscribe((g) => {
        this.group = g;
    })

    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        map((p) => +p.get('id'))
      ).subscribe((id) => {
      this.store.dispatch(getGroup({ id }))
    });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
