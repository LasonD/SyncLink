import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from "@ngrx/store";
import {
  combineLatest,
  distinctUntilChanged, race,
  ReplaySubject, startWith,
  Subject,
  takeUntil,
  withLatestFrom
} from "rxjs";
import {
  selectPrivateMessages,
  selectRoomError,
  selectRoomMessages,
  selectRoomMessagesError,
  selectRooms
} from "../store/rooms.selector";
import { ActivatedRoute } from "@angular/router";
import { Message } from "../../models/message.model";
import { getRoom, getMessages, sendMessage } from "../store/rooms.actions";
import { Room } from "../../models/room.model";
import { HttpErrorResponse } from "@angular/common/http";
import { AppState } from "../../store/app.reducer";
import { SignalRService } from "../../common/services/signalr.service";

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  messagePageSize = 25;

  groupId$: Subject<number> = new ReplaySubject<number>(1);
  currentUserId$: Subject<number> = new ReplaySubject<number>(1);
  otherUserId$: Subject<number> = new ReplaySubject<number>(1);
  roomId$: Subject<number> = new ReplaySubject<number>(1);
  isPrivate$ = new ReplaySubject<boolean>(1);
  room$: Subject<Room> = new ReplaySubject<Room>(1);
  sendMessage$ = new ReplaySubject<string>(1);

  messages: Message[];
  roomMessagesError: any;
  roomErrorMessage: string = null;
  messageText: string;

  constructor(private store: Store<AppState>,
              private activatedRoute: ActivatedRoute,
              private signalrService: SignalRService) {
  }

  ngOnInit() {
    this.sendMessage$
      .pipe(
        takeUntil(this.destroyed$),
        withLatestFrom(
          this.currentUserId$.pipe(startWith(undefined)),
          this.groupId$.pipe(startWith(undefined)),
          this.roomId$.pipe(startWith(undefined)),
          this.otherUserId$.pipe(startWith(undefined)),
          this.isPrivate$.pipe(startWith(undefined)),
        )
      )
      .subscribe(([text, senderId, groupId, roomId, otherUserId, isPrivate]) => {
        this.store.dispatch(sendMessage({
          senderId: senderId, roomId: roomId, isPrivate: isPrivate, otherUserId: otherUserId, payload: {
            roomId: roomId,
            text: text,
            groupId: groupId,
            recipientId: otherUserId
          }
        }));
        this.messageText = null;
      });

    this.resolveRouteAndUserIdentifiers();
    this.subscribeToErrors();
    this.resolveMessages();
    this.resolveRoom();
  }

  private subscribeToErrors() {
    this.store.select(selectRoomError).pipe(takeUntil(this.destroyed$), distinctUntilChanged())
      .subscribe(error => {
        if (!error) {
          this.roomErrorMessage = null;
          return;
        }

        if (error instanceof HttpErrorResponse) {
          switch (error.status) {
            case 404:
              this.roomErrorMessage = 'You do not have existing room. Send message to create it.';
              break;
            default:
              this.roomErrorMessage = 'Something went wrong when retrieving room.';
          }
        }
      });

    this.store.select(selectRoomMessagesError).pipe(takeUntil(this.destroyed$))
      .subscribe((error) => {
        this.roomMessagesError = error;
      });
  }

  private resolveMessages() {
    race(
      this.roomId$,
      this.otherUserId$,
    ).pipe(
      takeUntil(this.destroyed$),
      withLatestFrom(
        this.isPrivate$,
        this.groupId$,
        this.store.select(selectRoomMessages),
        this.store.select(selectPrivateMessages),
      )
    ).subscribe(([roomOrOtherUserId, isPrivate, groupId, roomMessages, privateMessages]) => {
      console.log('Room messages: ', roomMessages, 'Private messages: ', privateMessages, 'IsPrivate: ', isPrivate);
      const storeMessages = isPrivate ? privateMessages : roomMessages;
      const messagesById = storeMessages[roomOrOtherUserId];

      if (!messagesById?.messages) {
        this.store.dispatch(getMessages({
          isPrivate: isPrivate,
          groupId: groupId,
          otherUserId: isPrivate ? roomOrOtherUserId : null,
          roomId: isPrivate ? null : roomOrOtherUserId,
          pageNumber: 1,
          pageSize: this.messagePageSize
        }));
      } else {
        this.messages = messagesById.messages;
      }
    });

    combineLatest([
      this.store.select(selectRoomMessages),
      this.store.select(selectPrivateMessages),
    ]).pipe(
      takeUntil(this.destroyed$),
      withLatestFrom(
        this.isPrivate$,
        this.roomId$.pipe(startWith(undefined)),
        this.otherUserId$.pipe(startWith(undefined))),
    ).subscribe(([[roomMessages, privateMessages], isPrivate, roomId, otherUserId]) => {
      const storeMessages = isPrivate ? privateMessages : roomMessages;
      const messagesById = storeMessages[isPrivate ? otherUserId : roomId];
      if (messagesById?.messages) {
        this.messages = messagesById.messages;
      }
    });
  }

  private resolveRoom() {
    this.roomId$.pipe(
      takeUntil(this.destroyed$),
      withLatestFrom(this.groupId$, this.store.select(selectRooms)),
    ).subscribe(([roomId, groupId, rooms]) => {
      const room = rooms.find(r => r?.id === roomId);
      if (!room) {
        this.store.dispatch(getRoom({roomId: roomId, groupId: groupId}));
      } else {
        this.room$.next(room);
      }
    });

    this.store.select(selectRooms).pipe(
      takeUntil(this.destroyed$),
      withLatestFrom(this.roomId$),
    ).subscribe(([rooms, roomId]) => {
      if (!roomId) return;
      const room = rooms.find(r => r?.id === roomId);
      if (room) {
        this.room$.next(room);
      }
    });
  }

  private resolveRouteAndUserIdentifiers() {
    this.store.select('auth').pipe(takeUntil(this.destroyed$))
      .subscribe((state) => {
        this.currentUserId$.next(state.user?.userId);
      });

    this.activatedRoute.paramMap.pipe(takeUntil(this.destroyed$))
      .subscribe((p) => {
        const roomId = +p.get('roomId');
        const otherUserId = +p.get('userId');

        if (roomId) this.roomId$.next(roomId);
        if (otherUserId) this.otherUserId$.next(otherUserId);
        this.isPrivate$.next(!roomId);
      });

    this.activatedRoute.parent.paramMap.pipe(takeUntil(this.destroyed$))
      .subscribe((p) => {
        const groupId = +p.get('groupId');

        this.groupId$.next(groupId);
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }

  sendMessage() {
    if (!this.messageText) {
      return;
    }

    this.sendMessage$.next(this.messageText);
  }
}
