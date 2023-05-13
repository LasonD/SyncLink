import { Component, OnDestroy, OnInit } from '@angular/core';
import { RoomsState } from "../store/rooms.reducer";
import { Store } from "@ngrx/store";
import { combineLatest, distinctUntilChanged, Subject, takeUntil } from "rxjs";
import { selectRoomError, selectRoomMessages, selectRoomMessagesError, selectRooms } from "../store/rooms.selector";
import { ActivatedRoute } from "@angular/router";
import { Message } from "../../models/message.model";
import { getPrivateRoomByUser, getRoom, getRoomMessages } from "../store/rooms.actions";
import { AuthState } from "../../auth/store/auth.reducer";
import { Room } from "../../models/room.model";
import { filter } from "rxjs/operators";

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  messagePageSize = 25;

  currentUserId$: Subject<number> = new Subject<number>();
  otherUserId$: Subject<number> = new Subject<number>();
  roomId$: Subject<number> = new Subject<number>();
  groupId$: Subject<number> = new Subject<number>();

  room$: Subject<Room> = new Subject<Room>();

  roomId: number;
  messages: Message[];
  roomMessagesError: any;

  constructor(private store: Store<RoomsState>,
              private authStore: Store<AuthState>,
              private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.store.select(selectRooms)
      .subscribe(rooms => console.log('Rooms: ', rooms));

    this.store.select(selectRoomError).pipe(takeUntil(this.destroyed$), distinctUntilChanged())
      .subscribe(error => {
        alert(`You don't have a private room with user`);
      });

    this.store.select(selectRoomMessagesError).pipe(takeUntil(this.destroyed$))
      .subscribe((error) => {
        this.roomMessagesError = error;
      });

    this.resolveMessages();
    this.resolveRoom();
    this.resolveRouteAndUserIdentifiers();
  }

  private resolveMessages() {
    this.store.select(selectRoomMessages).pipe(takeUntil(this.destroyed$), distinctUntilChanged())
      .subscribe((messagesByRoom) => {
        if (!this.roomId) this.messages = [];
        this.messages = messagesByRoom[this.roomId]?.messages;
      })

    combineLatest([
      this.groupId$,
      this.room$,
      this.store.select(selectRoomMessages),
    ]).pipe(takeUntil(this.destroyed$), distinctUntilChanged((prev, cur) => {
      return prev[0] !== cur[0] || prev[1] !== cur[1];
    }))
      .subscribe(res => {
        const groupId = res[0];
        const room = res[1];
        const messagesByRoom = res[2];
        this.messages = messagesByRoom ? messagesByRoom[room.id]?.messages : null;

        if (!this.messages) {
          this.store.dispatch(getRoomMessages({
            groupId: groupId,
            roomId: room.id,
            pageNumber: 1,
            pageSize: this.messagePageSize
          }));
        }
      });
  }

  private resolveRoom() {
    combineLatest([
      this.groupId$,
      this.roomId$.pipe(filter(id => !!id)),
      this.store.select(selectRooms)
    ]).pipe(takeUntil(this.destroyed$), distinctUntilChanged((prev, cur) => {
      return prev[0] !== cur[0] || prev[1] !== cur[1];
    }))
      .subscribe(result => {
        const groupId = result[0];
        const roomId = result[1];
        const rooms = result[2];

        const room = rooms.find(r => r?.room?.id === roomId);

        if (!room) {
          this.store.dispatch(getRoom({roomId: roomId, groupId: groupId}));
        } else {
          this.room$.next(room.room);
        }
      });

    combineLatest([
      this.groupId$,
      this.otherUserId$.pipe(filter(id => !!id)),
      this.store.select(selectRooms)
    ]).pipe(takeUntil(this.destroyed$), distinctUntilChanged((prev, cur) => {
      return prev[0] !== cur[0] || prev[1] !== cur[1];
    }))
      .subscribe(result => {
        const groupId = result[0];
        const otherUserId = result[1];
        const rooms = result[2];

        const room = rooms.find(r => r.otherUserId === otherUserId);

        if (!room) {
          this.store.dispatch(getPrivateRoomByUser({userId: otherUserId, groupId: groupId}));
        } else {
          this.room$.next(room.room);
        }
      });

    this.store.select(selectRooms).pipe(takeUntil(this.destroyed$), distinctUntilChanged())
      .subscribe((rooms) => {
        if (!this.roomId) return;
        const room = rooms.find(r => r?.room?.id === this.roomId)?.room;
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
        this.roomId = roomId;
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
}
