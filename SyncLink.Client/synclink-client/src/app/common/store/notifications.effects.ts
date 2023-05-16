import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { tap } from "rxjs/operators";
import { ToastrService } from "ngx-toastr";
import {
  createRoomFailure,
  createRoomSuccess,
  getMessagesFailure, getRoomFailure, getRoomMembersFailure, sendMessageFailure,
} from "../../rooms/store/rooms.actions";
import { createGroupError, createGroupSuccess } from "../../groups/create-group/store/create-group.actions";
import {
  getGroupFailure,
  getGroupMembersFailure,
  getGroupRoomsFailure
} from "../../groups/group-hub/store/group-hub.actions";
import { LOGIN_FAILURE } from "../../auth/store/auth.actions";

@Injectable()
export class NotificationEffects {
  createRoomError$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createRoomFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Error creating a room');
        }
      )
    ), {dispatch: false}
  );

  createRoomSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createRoomSuccess),
      tap(({room}) => {
          this.notificationsService.success(`Room ${room.name} has been created.`, 'Success');
        }
      )
    ), {dispatch: false}
  );

  createGroupError$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createGroupError),
      tap(({error}) => {
          this.notificationsService.error(error, 'Error creating a group');
        }
      )
    ), {dispatch: false}
  );

  createGroupSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createGroupSuccess),
      tap(({group}) => {
          this.notificationsService.success(`Group ${group.name} has been created.`, 'Success');
        }
      )
    ), {dispatch: false}
  );

  getMessagesFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getMessagesFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when fetching messages. Try reloading a page.');
        }
      )
    ), {dispatch: false}
  );

  getRoomFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getRoomFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when fetching a room. Try reloading a page.');
        }
      )
    ), {dispatch: false}
  );

  getGroupFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroupFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when fetching a group. Try reloading a page.');
        }
      )
    ), {dispatch: false}
  );

  getRoomMembersFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getRoomMembersFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when fetching room members. Try reloading a page.');
        }
      )
    ), {dispatch: false}
  );

  getGroupRoomsFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroupRoomsFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when fetching group rooms. Try reloading a page.');
        }
      )
    ), {dispatch: false}
  );

  getGroupMembersFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getGroupMembersFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Something went wrong when fetching group members. Try reloading a page.');
        }
      )
    ), {dispatch: false}
  );

  sendMessageFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(sendMessageFailure),
      tap(({error}) => {
          this.notificationsService.error(error, 'Failed to send message');
        }
      )
    ), {dispatch: false}
  );

  loginFailure$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LOGIN_FAILURE),
      tap(({error}) => {
          this.notificationsService.error(error, 'Login has failed');
        }
      )
    ), {dispatch: false}
  );

  constructor(private actions$: Actions, private notificationsService: ToastrService) {}
}
