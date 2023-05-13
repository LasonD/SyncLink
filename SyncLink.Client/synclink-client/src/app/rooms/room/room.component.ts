import { Component, OnDestroy, OnInit } from '@angular/core';
import { RoomsState } from "../store/rooms.reducer";
import { Store } from "@ngrx/store";
import {
  combineLatest,
  defaultIfEmpty,
  distinctUntilChanged,
  ReplaySubject,
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
import { AuthState } from "../../auth/store/auth.reducer";
import { Room } from "../../models/room.model";
import { HttpErrorResponse } from "@angular/common/http";

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
  isPrivate$ = new Subject<boolean>();
  room$: Subject<Room> = new ReplaySubject<Room>(1);
  sendMessage$ = new ReplaySubject<string>(1);

  messages: Message[];
  roomMessagesError: any;
  roomErrorMessage: string = null;
  messageText: string;

  constructor(private store: Store<RoomsState>,
              private authStore: Store<AuthState>,
              private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.sendMessage$
      .pipe(
        takeUntil(this.destroyed$),
        withLatestFrom(
          this.currentUserId$.pipe(defaultIfEmpty(undefined)),
          this.groupId$.pipe(defaultIfEmpty(undefined)),
          this.roomId$.pipe(defaultIfEmpty(undefined)),
          this.otherUserId$.pipe(defaultIfEmpty(undefined)),
          this.isPrivate$.pipe(defaultIfEmpty(undefined)),
        )
      ).subscribe(([text, [senderId, groupId, roomId, otherUserId, isPrivate]]) => {
      this.store.dispatch(sendMessage({
        senderId: senderId, roomId: roomId, isPrivate: isPrivate, otherUserId: otherUserId, payload: {
          roomId: roomId,
          text: text,
          groupId: groupId,
          recipientId: otherUserId
        }
      }))
    });

    this.subscribeToErrors();
    this.resolveMessages();
    this.resolveRoom();
    this.resolveRouteAndUserIdentifiers();
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
    combineLatest([
      this.roomId$,
      this.otherUserId$,
    ]).pipe(
      takeUntil(this.destroyed$),
      withLatestFrom(
        this.isPrivate$,
        this.groupId$,
        this.store.select(selectRoomMessages),
        this.store.select(selectPrivateMessages),
      )
    ).subscribe(([[roomId, otherUserId], isPrivate, groupId, roomMessages, privateMessages]) => {
      const storeMessages = isPrivate ? privateMessages : roomMessages;
      const messagesById = storeMessages[isPrivate ? otherUserId : roomId];

      if (!messagesById?.messages) {
        this.store.dispatch(getMessages({
          isPrivate: isPrivate,
          groupId: groupId,
          otherUserId: otherUserId,
          roomId: roomId,
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
        this.roomId$.pipe(defaultIfEmpty(undefined)),
        this.otherUserId$.pipe(defaultIfEmpty(undefined))),
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
    this.authStore.pipe(takeUntil(this.destroyed$))
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
