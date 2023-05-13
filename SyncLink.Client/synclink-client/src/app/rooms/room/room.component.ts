import { Component, OnDestroy, OnInit } from '@angular/core';
import { RoomsState } from "../store/rooms.reducer";
import { Store } from "@ngrx/store";
import { Subject, takeUntil } from "rxjs";
import { selectRoomMessages, selectRoomMessagesError } from "../store/rooms.selector";
import { ActivatedRoute } from "@angular/router";
import { Message } from "../../models/message.model";
import { getRoomMessages } from "../store/rooms.actions";
import { AuthState } from "../../auth/store/auth.reducer";

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  messagePageSize = 25;

  userId: number;

  roomId: number;
  groupId: number;

  messagesByRoomId: { [roomId: number]: Message[] };
  messages: Message[];
  roomMessagesError: any;

  constructor(private store: Store<RoomsState>,
              private authStore: Store<AuthState>,
              private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.store.select(selectRoomMessagesError).pipe(takeUntil(this.destroyed$))
      .subscribe((error) => {
        console.log('Error', error)
        this.roomMessagesError = error;
      })

    this.authStore.pipe(takeUntil(this.destroyed$))
      .subscribe((state) => {
        this.userId = state.user?.userId;
      })

    this.activatedRoute.paramMap.pipe(takeUntil(this.destroyed$))
      .subscribe((p) => {
        this.roomId = +p.get('roomId');
        this.groupId = +p.get('groupId');
        this.getMessages();
      });

    this.store.select(selectRoomMessages)
      .pipe(takeUntil(this.destroyed$))
      .subscribe((messagesByRoomId) => {
        this.messagesByRoomId = messagesByRoomId;
        this.getMessages();
      });
  }

  private getMessages() {
    this.messages = this.messagesByRoomId ? this.messagesByRoomId[this.roomId] : null;
    if (!this.messages && !this.roomMessagesError && this.messagePageSize-- > 0) {
      this.store.dispatch(getRoomMessages({ groupId: this.groupId, roomId: this.roomId, pageNumber: 1, pageSize: this.messagePageSize }));
    }
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
