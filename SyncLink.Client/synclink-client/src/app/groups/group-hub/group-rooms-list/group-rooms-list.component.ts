import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil, withLatestFrom } from "rxjs";
import { GroupHubState } from "../store/group-hub.reducer";
import { Store } from "@ngrx/store";
import { getGroupRooms } from "../store/group-hub.actions";
import { selectGroupHubRooms } from "../store/group-hub.selectors";
import { ActivatedRoute, Router } from "@angular/router";
import { selectUserId } from "../../../auth/store/auth.selectors";
import { Room } from "../../../models/room.model";

@Component({
  selector: 'app-group-rooms-list',
  templateUrl: './group-rooms-list.component.html',
  styleUrls: ['./group-rooms-list.component.scss']
})
export class GroupRoomsListComponent implements OnInit, OnDestroy {
  @Input() groupId: number;
  @Input() pageNumber: number = 1;
  @Input() pageSize: number = 25;

  selectedRoomId: number;

  destroyed$: Subject<boolean> = new Subject<boolean>();
  rooms: Room[] = [];

  constructor(private store: Store<GroupHubState>,
              private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  ngOnInit() {
    this.groupId = +this.activatedRoute.snapshot.paramMap.get('groupId');

    this.store.dispatch(getGroupRooms({ id: this.groupId, pageNumber: this.pageNumber, pageSize: this.pageSize  }));

    this.store.select(selectGroupHubRooms)
      .pipe(takeUntil(this.destroyed$), withLatestFrom(this.store.select(selectUserId)))
      .subscribe(([pages, userId]) => {
        this.rooms = pages.flatMap((p) => p.entities).filter(m => m.id !== userId);
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }

  openRoom(roomId: number) {
    this.selectedRoomId = roomId;
    this.router.navigate(['rooms', roomId], { relativeTo: this.activatedRoute });
  }
}
