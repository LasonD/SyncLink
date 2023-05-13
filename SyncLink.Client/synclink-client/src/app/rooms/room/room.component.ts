import { Component, OnDestroy, OnInit } from '@angular/core';
import { RoomsState } from "../store/rooms.reducer";
import { Store } from "@ngrx/store";
import { Subject, takeUntil } from "rxjs";
import { selectRoomMessages } from "../store/rooms.selector";
import { ActivatedRoute } from "@angular/router";
import { Message } from "../../models/message.model";
import { getRoomMessages } from "../store/rooms.actions";

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  messagePageSize = 25;

  roomId: number;
  groupId: number;

  messagesByRoomId: { [roomId: number]: Message[] };
  messages: Message[];

  constructor(private store: Store<RoomsState>,
              private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.activatedRoute.paramMap.pipe(takeUntil(this.destroyed$))
      .subscribe((p) => {
        this.roomId = +p.get('roomId');
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
    this.messages  = this.messagesByRoomId[this.roomId];
    if (!this.messages) {
      this.store.dispatch(getRoomMessages({ roomId: this.roomId, pageNumber: 1, pageSize: this.messagePageSize }));
    }
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
