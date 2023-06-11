import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, switchMap, tap } from "rxjs/operators";
import { Page } from "../../../models/pagination.model";
import { environment } from "../../../environments/environment";
import { of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { ToastrService } from "ngx-toastr";
import {
  getGroupJoinRequests,
  getGroupJoinRequestsFailure,
  getGroupJoinRequestsSuccess, updateGroupJoinRequest, updateGroupJoinRequestFailure, updateGroupJoinRequestSuccess
} from "./join-requests.actions";
import { GroupJoinRequest } from "./join-requests.reducer";

@Injectable()
export class JoinRequestsEffects {
  getJoinRequests$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroupJoinRequests),
      switchMap(({ groupId }) =>
        this.http.get<Page<GroupJoinRequest>>(`${environment.apiBaseUrl}/api/groups/${groupId}/join-requests`).pipe(
          map((joinRequests) => {
            return getGroupJoinRequestsSuccess({joinRequests});
          }),
          catchError((error) => of(getGroupJoinRequestsFailure({ error })))
        )
      )
    )
  );

  getJoinRequestFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroupJoinRequestsFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when getting group join requests.');
        }
      )
    ), {dispatch: false}
  );

  updateJoinRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateGroupJoinRequest),
      switchMap(({ groupId, joinRequestId, updatedState }) =>
        this.http.put<GroupJoinRequest>(`${environment.apiBaseUrl}/api/groups/${groupId}/join-requests/${joinRequestId}`, updatedState).pipe(
          map((state) => {
            return updateGroupJoinRequestSuccess({ updatedRequest: state });
          }),
          catchError((error) => of(updateGroupJoinRequestFailure({ error })))
        )
      )
    )
  );

  updateJoinRequestFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateGroupJoinRequestFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when updating group join request.');
        }
      )
    ), {dispatch: false}
  );

  updateJoinRequestSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateGroupJoinRequestSuccess),
      tap(({updatedRequest}) => {
          this.notificationsService.success(`Updated state: ${updatedRequest.status?.toLocaleString()}`, 'Group join request updated:');
        }
      )
    ), {dispatch: false}
  );

  constructor(private actions$: Actions, private http: HttpClient, private notificationsService: ToastrService) {}
}
