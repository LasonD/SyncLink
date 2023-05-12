import { createAction, props } from "@ngrx/store";
import { Room } from "../../models/group.model";

export const getRoom = createAction(
  '[Rooms] Get Room',
  props<{ id: number }>()
);

export const getRoomSuccess = createAction(
  '[Rooms] Get Room Success',
  props<{ room: Room }>()
);

export const getRoomFailure = createAction(
  '[Rooms] Get Room Failure',
  props<{ error: any }>()
);

export const getPrivateRoomByUser = createAction(
  '[Rooms] Get Private Room',
  props<{ userId: number }>()
);

export const createRoom = createAction(
  '[Rooms] Create Room',
  props<{ name: string, memberIds: number[], isPrivate: boolean }>()
);

export const createRoomSuccess = createAction(
  '[Rooms] Create Room Success',
  props<{ room: Room }>()
);

export const createRoomFailure = createAction(
  '[Rooms] Create Room Failure',
  props<{ error: any }>()
);
