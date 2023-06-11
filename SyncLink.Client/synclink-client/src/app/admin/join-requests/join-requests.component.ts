import { Component, OnDestroy } from '@angular/core';
import { GroupJoinRequest, GroupJoinRequestStatus } from "./store/join-requests.reducer";
import { getGroupJoinRequests, updateGroupJoinRequest } from "./store/join-requests.actions";
import { AppState } from "../../store/app.reducer";
import { Store } from "@ngrx/store";
import { joinRequestSelectors } from "./store/join-requests.selectors";
import { selectCurrentGroupId } from "../../groups/group-hub/store/group-hub.selectors";
import { Subject, takeUntil } from "rxjs";
import { filter, take } from "rxjs/operators";

@Component({
  selector: 'app-join-requests',
  templateUrl: './join-requests.component.html',
  styleUrls: ['./join-requests.component.scss']
})
export class JoinRequestsComponent implements OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  joinRequests$ = this.store.select(joinRequestSelectors.selectAll).pipe(takeUntil(this.destroyed$), filter(requests => !!requests));
  groupId$ = this.store.select(selectCurrentGroupId).pipe(takeUntil(this.destroyed$), filter(id => !!id));
  GroupJoinRequestStatus = GroupJoinRequestStatus;

  constructor(private store: Store<AppState>) {
  }

  ngOnInit(): void {
    this.groupId$
      .subscribe(groupId => {
        this.store.dispatch(getGroupJoinRequests({ groupId }));
      })
  }

  approve(joinRequest: GroupJoinRequest): void {
    this.updateJoinRequest(joinRequest, GroupJoinRequestStatus.Accepted);
  }

  reject(joinRequest: GroupJoinRequest): void {
    this.updateJoinRequest(joinRequest, GroupJoinRequestStatus.Rejected);
  }

  private updateJoinRequest(joinRequest: GroupJoinRequest, newStatus: GroupJoinRequestStatus) {
    this.groupId$.pipe(take(1))
      .subscribe(groupId => {
        this.store.dispatch(updateGroupJoinRequest({
          joinRequestId: joinRequest.id, groupId, updatedState: {
            newState: newStatus
          }
        }))
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
