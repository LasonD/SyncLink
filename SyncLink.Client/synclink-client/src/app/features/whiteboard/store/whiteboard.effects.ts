import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import {
  closeGroup,
  getGroup,
  getGroupMembers,
  getGroupMembersFailure,
  getGroupMembersSuccess,
  getGroupRooms,
  getGroupRoomsFailure,
  getGroupRoomsSuccess,
  openGroup
} from "../../../groups/group-hub/store/group-hub.actions";
import { catchError, map, mergeMap } from "rxjs/operators";
import { GroupMember } from "../../../models/group.model";
import { environment } from "../../../environments/environment";
import { of } from "rxjs";
import { Page } from "../../../models/pagination.model";
import { Room } from "../../../models/room.model";
import { HttpClient } from "@angular/common/http";
import { SignalRService } from "../../../common/services/signalr.service";
import { Whiteboard } from "./whiteboard.reducer";
import { getWhiteboardFailure, getWhiteboardSuccess } from "./whiteboard.actions";

@Injectable()
export class WhiteboardEffects {
  getWhiteboard$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroup),
      mergeMap(({ id }: { id: number }) => {
          return this.http.get<Whiteboard>(`${environment.apiBaseUrl}/api/features/whiteboard/${id}`).pipe(
            map((whiteboard) => {
              return getWhiteboardSuccess({whiteboard});
            }),
            catchError((error) => of(getWhiteboardFailure({error})))
          );
        }
      )
    )
  );

  whiteboardChanged$ = createEffect(() =>
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
