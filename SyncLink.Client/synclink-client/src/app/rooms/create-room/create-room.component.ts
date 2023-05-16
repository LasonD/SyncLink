import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { RoomsState } from "../store/rooms.reducer";
import { Store } from "@ngrx/store";
import { selectGroupMembers } from "../../groups/group-hub/store/group-hub.selectors";
import { Observable, Subject, takeUntil, withLatestFrom } from "rxjs";
import { GroupMember } from "../../models/group.model";
import { selectUserId } from "../../auth/store/auth.selectors";
import { map } from "rxjs/operators";
import { createRoom } from "../store/rooms.actions";

@Component({
  selector: 'app-create-room',
  templateUrl: './create-room.component.html',
  styleUrls: ['./create-room.component.scss']
})
export class CreateRoomComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();

  createRoomForm: FormGroup;
  members$: Observable<GroupMember[]>;

  constructor(private fb: FormBuilder, private store: Store<RoomsState>) {

  }

  ngOnInit() {
    this.createRoomForm = this.fb.group({
      roomName: ['', Validators.required],
      description: [''],
      users: ['', Validators.required]
    });

    this.members$ = this.store.select(selectGroupMembers).pipe(
      takeUntil(this.destroyed$),
      withLatestFrom(this.store.select(selectUserId)),
      map(([members, currentUserId]) => {
        return members.filter(m => m.id !== currentUserId);
      })
    );
  }

  onSubmit() {
    if (!this.createRoomForm.valid) {
      return;
    }

    const value = this.createRoomForm.value;

    const name = value.name;
    const description = value.description;
    const memberIds = value.users.map(u => u.id);

    this.store.dispatch(createRoom({ name, memberIds, description }));
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
