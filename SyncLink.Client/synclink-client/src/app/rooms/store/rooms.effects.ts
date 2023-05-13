import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import {
  getPrivateRoomByUser,
  getRoom,
  getRoomFailure, getRoomMembers, getRoomMembersSuccess,
  getRoomMessages,
  getRoomMessagesFailure, getRoomMessagesSuccess,
  getRoomSuccess
} from "./rooms.actions";
import { catchError, map, mergeMap } from "rxjs/operators";
import { environment } from "../../environments/environment";
import { Room, RoomMember } from "../../models/room.model";
import { of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Page } from "../../models/pagination.model";
import { Message } from "../../models/message.model";

@Injectable()
export class RoomEffects {
  getRoom$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getRoom),
      mergeMap(({ groupId, roomId }) => {
          return this.http.get<Room>(`${environment.apiBaseUrl}/api/groups/${groupId}/rooms/${roomId}`).pipe(
            map((room: Room) => {
              return getRoomSuccess({ room });
            }),
            catchError((error) => of(getRoomFailure({error})))
          );
        }
      )
    )
  );

  getPrivateRoom$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getPrivateRoomByUser),
      mergeMap(({ groupId, userId }) => {
          return this.http.get<Room>(`${environment.apiBaseUrl}/api/groups/${groupId}/members/${userId}/private`).pipe(
            map((room: Room) => {
              return getRoomSuccess({ room });
            }),
            catchError((error) => of(getRoomFailure({error})))
          );
        }
      )
    )
  );

  getRoomMessages$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getRoomMessages),
      mergeMap(({ groupId, roomId, pageNumber, pageSize }) => {
          return this.http.get<Page<Message>>(`${environment.apiBaseUrl}/api/groups/${groupId}/rooms/${roomId}/messages?pageNumber=${pageNumber}&pageSize=${pageSize}`).pipe(
            map((messages: Page<Message>) => {
              return getRoomMessagesSuccess({ roomId: roomId, messages: messages });
            }),
            catchError((error) => of(getRoomMessagesFailure({error})))
          );
        }
      )
    )
  );

  getRoomMembers$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getRoomMembers),
      mergeMap(({ groupId, roomId, pageNumber, pageSize }) => {
          return this.http.get<Page<RoomMember>>(`${environment.apiBaseUrl}/api/groups/${groupId}/rooms/${roomId}/members?pageNumber=${pageNumber}&pageSize=${pageSize}`).pipe(
            map((members: Page<RoomMember>) => {
              return getRoomMembersSuccess({ roomId: roomId, members: members });
            }),
            catchError((error) => of(getRoomMessagesFailure({error})))
          );
        }
      )
    )
  );

  constructor(private actions$: Actions, private http: HttpClient) {
  }
}
