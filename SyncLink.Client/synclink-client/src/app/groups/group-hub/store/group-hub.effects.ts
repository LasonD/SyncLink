import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap } from "rxjs/operators";
import { Group, GroupMember } from "../../../models/group.model";
import { environment } from "../../../environments/environment";
import { of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import {
  getGroup,
  getGroupFailure,
  getGroupMembers,
  getGroupMembersFailure, getGroupMembersSuccess,
  getGroupSuccess, openGroup
} from "./group-hub.actions";
import { Page } from "../../../models/pagination.model";
import { SignalRService } from "../../../common/services/signalr.service";

@Injectable()
export class GroupHubEffects {
  getGroup$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroup),
      mergeMap(({ id }: { id: number }) => {
          return this.http.get<Group>(`${environment.apiBaseUrl}/api/groups/${id}`).pipe(
            map((group) => {
              return getGroupSuccess({group});
            }),
            catchError((error) => of(getGroupFailure({error})))
          );
        }
      )
    )
  );

  getGroupMembers$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroupMembers),
      mergeMap(({ id }: { id: number }) => {
          return this.http.get<Page<GroupMember>>(`${environment.apiBaseUrl}/api/groups/${id}/members`).pipe(
            map((page) => {
              return getGroupMembersSuccess({ membersPage: page});
            }),
            catchError((error) => of(getGroupMembersFailure({error})))
          );
        }
      )
    )
  );

  openGroup$ = createEffect(() =>
    this.actions$.pipe(
      ofType(openGroup),
      mergeMap(({ groupId }) => {
          return this.signalrService.groupOpened(groupId)
            .then(() => of())
            .catch(err => of()) // todo: ADD NOTIFICATION SERVICE
        }
      )
    ), { dispatch: false }
  );

  closeGroup$ = createEffect(() =>
    this.actions$.pipe(
      ofType(openGroup),
      mergeMap(({ groupId }) => {
          return this.signalrService.groupClosed(groupId)
            .then(() => of())
            .catch(err => of()) // todo: ADD NOTIFICATION SERVICE
        }
      )
    ), { dispatch: false }
  );

  constructor(private actions$: Actions, private http: HttpClient, private signalrService: SignalRService) {}
}
