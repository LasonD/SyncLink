import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, switchMap } from "rxjs/operators";
import { Group } from "../../models/group.model";
import { environment } from "../../../environments/environment";
import { of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { getGroupComplete, getGroupCompleteFailure, getGroupCompleteSuccess } from "./group-hub.actions";

@Injectable()
export class GroupSearchEffects {
  searchGroups$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroupComplete),
      switchMap(({ id }: { id: string }) =>
        this.http.get<Group>(`${environment.apiBaseUrl}/api/groups/${id}`).pipe(
          map((group) => {
            return getGroupCompleteSuccess({group});
          }),
          catchError((error) => of(getGroupCompleteFailure({ error })))
        )
      )
    )
  );

  constructor(private actions$: Actions, private http: HttpClient) {}
}
