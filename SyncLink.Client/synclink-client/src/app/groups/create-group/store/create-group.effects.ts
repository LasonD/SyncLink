import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { HttpClient } from '@angular/common/http';
import { catchError, map, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { createGroup, createGroupError, createGroupSuccess } from "./create-group.actions";
import { environment } from "../../../environments/environment";
import { CreateGroupDto, Group } from "../../models/group.model";

@Injectable()
export class CreateGroupEffects {
  createGroup$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(createGroup),
      switchMap((createGroup: CreateGroupDto) =>
        this.http.post<Group>(`${environment.apiBaseUrl}/api/groups`, createGroup).pipe(
          map((group) => createGroupSuccess({group})),
          catchError((error) => of(createGroupError({error})))
        ))
    );
  });

  constructor(private actions$: Actions, private http: HttpClient) {}
}
