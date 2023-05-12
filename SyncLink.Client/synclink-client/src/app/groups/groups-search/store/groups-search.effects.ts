import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { HttpClient } from '@angular/common/http';
import { catchError, map, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { searchGroups, searchGroupsFailure, searchGroupsSuccess } from "./groups-search.actions";
import { Group, GroupSearchMode } from "../../models/group.model";
import { environment } from "../../../environments/environment";
import { Page } from "../../models/pagination.model";

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

  constructor(private actions$: Actions, private http: HttpClient) {}
}
