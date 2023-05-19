import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap } from "rxjs/operators";
import { environment } from "../../../environments/environment";
import { of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { SignalRService } from "../../../common/services/signalr.service";
import { Whiteboard } from "./whiteboard.reducer";
import {
  createWhiteboard, createWhiteboardFailure, createWhiteboardSuccess,
  getWhiteboard,
  getWhiteboardFailure, getWhiteboards, getWhiteboardsFailure, getWhiteboardsSuccess,
  getWhiteboardSuccess,
  whiteboardUpdated, whiteboardUpdatedFailure,
  whiteboardUpdatedSuccess
} from "./whiteboard.actions";

@Injectable()
export class WhiteboardEffects {
  getWhiteboard$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getWhiteboard),
      mergeMap(({ id, groupId }) => {
          return this.http.get<Whiteboard>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/whiteboards/${id}`).pipe(
            map((whiteboard) => {
              return getWhiteboardSuccess({whiteboard});
            }),
            catchError((error) => of(getWhiteboardFailure({error})))
          );
        }
      )
    )
  );

  getWhiteboards$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getWhiteboards),
      mergeMap(({ groupId }) => {
          return this.http.get<Whiteboard[]>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/whiteboards`).pipe(
            map((whiteboards) => {
              return getWhiteboardsSuccess({whiteboards});
            }),
            catchError((error) => of(getWhiteboardsFailure({error})))
          );
        }
      )
    )
  );

  createWhiteboard$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createWhiteboard),
      mergeMap(({groupId, name}) => {
          return this.http.post<Whiteboard>(`${environment.apiBaseUrl}/api/groups/${groupId}/features/whiteboards`, {
            name
          }).pipe(
            map((whiteboard) => {
              return createWhiteboardSuccess({whiteboard});
            }),
            catchError((error) => of(createWhiteboardFailure({error})))
          );
        }
      )
    )
  );

  whiteboardUpdated$ = createEffect(() =>
    this.actions$.pipe(
      ofType(whiteboardUpdated),
      mergeMap(({ id, groupId, changes }) => {
          return this.signalrService.whiteboardUpdated(groupId, id, changes)
            .then(() => whiteboardUpdatedSuccess({ id }))
            .catch(err => whiteboardUpdatedFailure({ error: err }))
        }
      )
    ),
  );

  constructor(private actions$: Actions, private http: HttpClient, private signalrService: SignalRService) {}
}
