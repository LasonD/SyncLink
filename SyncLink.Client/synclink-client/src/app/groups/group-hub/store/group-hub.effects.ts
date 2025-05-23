import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap } from "rxjs/operators";
import { Group, GroupMember } from "../../../models/group.model";
import { environment } from "../../../environments/environment";
import { of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import {
  closeGroup,
  getGroup,
  getGroupFailure,
  getGroupMembers,
  getGroupMembersFailure, getGroupMembersSuccess, getGroupRooms, getGroupRoomsFailure, getGroupRoomsSuccess,
  getGroupSuccess, openGroup
} from "./group-hub.actions";
import { Page } from "../../../models/pagination.model";
import { SignalRService } from "../../../common/services/signalr.service";
import { Room } from "../../../models/room.model";

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
      mergeMap(({id}: { id: number }) => {
          return this.http.get<Page<GroupMember>>(`${environment.apiBaseUrl}/api/groups/${id}/members`).pipe(
            map((page) => {
              return getGroupMembersSuccess({membersPage: page});
            }),
            catchError((error) => of(getGroupMembersFailure({error})))
          );
        }
      )
    )
  );

  getGroupRooms$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroupRooms),
      mergeMap(({id}: { id: number }) => {
          return this.http.get<Page<Room>>(`${environment.apiBaseUrl}/api/groups/${id}/rooms`).pipe(
            map((page) => {
              return getGroupRoomsSuccess({roomsPage: page});
            }),
            catchError((error) => of(getGroupRoomsFailure({error})))
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
      ofType(closeGroup),
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
