import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, switchMap } from "rxjs/operators";
import { Group, GroupMember } from "../../models/group.model";
import { environment } from "../../../environments/environment";
import { of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import {
  getGroup,
  getGroupFailure,
  getGroupMembers,
  getGroupMembersFailure, getGroupMembersSuccess,
  getGroupSuccess
} from "./group-hub.actions";
import { Page } from "../../models/pagination.model";

@Injectable()
export class GroupHubEffects {
  getGroup$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroup),
      switchMap(({ id }: { id: number }) => {
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
      switchMap(({ id }: { id: number }) => {
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

  constructor(private actions$: Actions, private http: HttpClient) {}
}
