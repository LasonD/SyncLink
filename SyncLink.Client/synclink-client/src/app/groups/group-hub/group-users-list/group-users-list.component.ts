import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from "rxjs";
import { GroupHubState } from "../store/group-hub.reducer";
import { Store } from "@ngrx/store";
import { getGroupMembers } from "../store/group-hub.actions";
import { GroupMember } from "../../../models/group.model";
import { selectGroupHubMembers } from "../store/group-hub.selectors";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-group-users-list',
  templateUrl: './group-users-list.component.html',
  styleUrls: ['./group-users-list.component.scss']
})
export class GroupUsersListComponent implements OnInit, OnDestroy {
  @Input() groupId: number;
  @Input() pageNumber: number = 1;
  @Input() pageSize: number = 25;

  selectedMemberId: number;

  destroyed$: Subject<boolean> = new Subject<boolean>();
  members: GroupMember[] = [];

  constructor(private store: Store<GroupHubState>,
              private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  ngOnInit() {
    this.groupId = +this.activatedRoute.snapshot.paramMap.get('groupId');

    this.store.dispatch(getGroupMembers({ id: this.groupId, pageNumber: this.pageNumber, pageSize: this.pageSize  }));

    this.store.select(selectGroupHubMembers)
      .pipe(takeUntil(this.destroyed$))
      .subscribe((pages) => {
        console.log(pages);
        this.members = pages.flatMap((p) => p.entities);
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }

  openPrivateRoom(userId: number) {
    this.selectedMemberId = userId;
    this.router.navigate(['members', userId, 'private'], { relativeTo: this.activatedRoute });
  }
}
