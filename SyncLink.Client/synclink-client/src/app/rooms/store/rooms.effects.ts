import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import {
  getPrivateRoomByUser,
  getRoom,
  getRoomFailure, getRoomMembers, getRoomMembersSuccess,
  getMessages,
  getMessagesFailure, getMessagesSuccess,
  getRoomSuccess, sendMessage, sendMessageFailure, sendMessageSuccess, getRoomMembersFailure
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

  getMessages$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getMessages),
      mergeMap(({groupId, roomId, pageNumber, pageSize, otherUserId, isPrivate}) => {
          const url = isPrivate ?
            `${environment.apiBaseUrl}/api/groups/${groupId}/members/${otherUserId}/messages?pageNumber=${pageNumber}&pageSize=${pageSize}` :
            `${environment.apiBaseUrl}/api/groups/${groupId}/rooms/${roomId}/messages?pageNumber=${pageNumber}&pageSize=${pageSize}`

          return this.http.get<Page<Message>>(url).pipe(
            map((messages: Page<Message>) => ({
              ...messages,
              entities: messages?.entities.map(m => ({
                ...m,
                creationDate: new Date(m.creationDate),
                editedDateTime: m.editedDateTime ? new Date(m.editedDateTime) : null,
              }))
            })),
            map((messages: Page<Message>) => {
              return getMessagesSuccess({
                roomId: roomId,
                messages: messages,
                otherUserId: otherUserId,
                isPrivate: isPrivate
              });
            }),
            catchError((error) => of(getMessagesFailure({error})))
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
            catchError((error) => of(getRoomMembersFailure({error})))
          );
        }
      )
    )
  );

  sendMessage$ = createEffect(() =>
    this.actions$.pipe(
      ofType(sendMessage),
      mergeMap(({ roomId, otherUserId, isPrivate, payload }) => {
          return this.http.post<Message>(`${environment.apiBaseUrl}/api/messages`, payload).pipe(
            map((message: Message) => {
              return sendMessageSuccess({ roomId: roomId, otherUserId: otherUserId, isPrivate: isPrivate, message: message });
            }),
            catchError((error) => of(sendMessageFailure({error})))
          );
        }
      )
    )
  );

  constructor(private actions$: Actions, private http: HttpClient) {
  }
}

export interface SendMessageData {
  groupId: number;
  text: string;
  roomId?: number;
  recipientId?: number;
}

