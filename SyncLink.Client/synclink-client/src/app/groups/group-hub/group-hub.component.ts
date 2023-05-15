import { Component, OnDestroy, OnInit } from '@angular/core';
import { GroupsState } from "../store/groups.reducer";
import { Store } from "@ngrx/store";
import { closeGroup, getGroup, openGroup } from "./store/group-hub.actions";
import { ActivatedRoute } from "@angular/router";
import { Subject, takeUntil } from "rxjs";
import { map } from "rxjs/operators";
import { selectGroupHubGroup } from "./store/group-hub.selectors";
import { Group } from "../../models/group.model";

@Component({
  selector: 'app-group-hub',
  templateUrl: './group-hub.component.html',
  styleUrls: ['./group-hub.component.scss']
})
export class GroupHubComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  group: Group;

  constructor(private store: Store<GroupsState>,
              private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.store.select(selectGroupHubGroup)
      .pipe(
        takeUntil(this.destroyed$)
      ).subscribe((g) => {
        this.group = g;
        this.store.dispatch(openGroup({ groupId: this.group?.id }));
    })

    const groupId = +this.activatedRoute.snapshot.paramMap.get('groupId');

    this.store.dispatch(getGroup({ id: groupId }))

    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        map((p) => +p.get('groupId'))
      ).subscribe((id) => {
      this.store.dispatch(getGroup({ id }))
    });
  }

  ngOnDestroy() {
    this.store.dispatch(closeGroup({ groupId: this.group?.id }));
    this.destroyed$.next(true);
  }
}
