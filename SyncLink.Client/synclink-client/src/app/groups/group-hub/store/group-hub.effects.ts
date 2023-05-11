import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, switchMap } from "rxjs/operators";
import { Group } from "../../models/group.model";
import { environment } from "../../../environments/environment";
import { of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { getGroup, getGroupFailure, getGroupSuccess } from "./group-hub.actions";

@Injectable()
export class GroupHubEffects {
  searchGroups$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroup),
      switchMap(({ id }: { id: number }) => {
        console.log('Getting group ', id);
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

  constructor(private actions$: Actions, private http: HttpClient) {}
}
