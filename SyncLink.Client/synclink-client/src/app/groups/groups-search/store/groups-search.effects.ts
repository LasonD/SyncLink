import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { HttpClient } from '@angular/common/http';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import {
  searchGroups,
  searchGroupsFailure,
  searchGroupsSuccess,
  sendGroupJoinRequest, sendGroupJoinRequestFailure,
  sendGroupJoinRequestSuccess
} from "./groups-search.actions";
import { Group, GroupSearchMode } from "../../../models/group.model";
import { environment } from "../../../environments/environment";
import { Page } from "../../../models/pagination.model";
import { GroupJoinRequestState } from "./groups-search.reducer";
import { ToastrService } from "ngx-toastr";

@Injectable()
export class GroupSearchEffects {
  searchGroups$ = createEffect(() =>
    this.actions$.pipe(
      ofType(searchGroups),
      switchMap(({ searchQuery, groupSearchMode }: { searchQuery: string, groupSearchMode: GroupSearchMode }) =>
        this.http.get<Page<Group>>(`${environment.apiBaseUrl}/api/groups/search?searchQuery=${searchQuery}&groupSearchMode=${groupSearchMode}`).pipe(
          map((groups) => {
            return searchGroupsSuccess({groups});
          }),
          catchError((error) => of(searchGroupsFailure({ error })))
        )
      )
    )
  );

  sendGroupJoinRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(sendGroupJoinRequest),
      switchMap(({ groupId, request }) =>
        this.http.post<GroupJoinRequestState>(`${environment.apiBaseUrl}/api/groups/${groupId}/join-requests`, request).pipe(
          map((state) => {
            return sendGroupJoinRequestSuccess({ joinRequestState: state });
          }),
          catchError((error) => of(sendGroupJoinRequestFailure({ error })))
        )
      )
    )
  );

  sendGroupJoinRequestFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(sendGroupJoinRequestFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when sending group join request.');
        }
      )
    ), {dispatch: false}
  );

  sendGroupJoinRequestSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(sendGroupJoinRequestSuccess),
      tap(({joinRequestState}) => {
          this.notificationsService.success(joinRequestState.status?.toLocaleString(), 'Group join request sent:');
        }
      )
    ), {dispatch: false}
  );

  constructor(private actions$: Actions, private http: HttpClient, private notificationsService: ToastrService) {}
}
