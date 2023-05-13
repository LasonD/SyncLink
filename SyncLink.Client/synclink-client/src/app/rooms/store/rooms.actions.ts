import { createAction, props } from "@ngrx/store";
import { Room, RoomMember } from "../../models/room.model";
import { Message } from "../../models/message.model";
import { Page } from "../../models/pagination.model";

export const getRoom = createAction(
  '[Rooms] Get Room',
  props<{ groupId: number, roomId: number }>()
);

export const getRoomSuccess = createAction(
  '[Rooms] Get Room Success',
  props<{ otherUserId?: number, room: Room }>()
);

export const getRoomMessages = createAction(
  '[Rooms] Get Room Messages',
  props<{ groupId: number, roomId: number, pageNumber: number, pageSize: number }>()
);

export const getRoomMessagesSuccess = createAction(
  '[Rooms] Get Room Messages Success',
  props<{ roomId: number, messages: Page<Message> }>()
);

export const getRoomMessagesFailure = createAction(
  '[Rooms] Get Room Messages Failure',
  props<{ error: any }>()
);

export const getRoomMembers = createAction(
  '[Rooms] Get Room Members',
  props<{ groupId: number, roomId: number, pageNumber: number, pageSize: number }>()
);

export const getRoomMembersFailure = createAction(
  '[Rooms] Get Room Members Failure',
  props<{ error: any }>()
);

export const getRoomMembersSuccess = createAction(
  '[Rooms] Get Room Members Success',
  props<{ roomId: number, members: Page<RoomMember> }>()
);

export const getRoomFailure = createAction(
  '[Rooms] Get Room Failure',
  props<{ error: any }>()
);

export const getPrivateRoomByUser = createAction(
  '[Rooms] Get Private Room',
  props<{ groupId: number, userId: number }>()
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
