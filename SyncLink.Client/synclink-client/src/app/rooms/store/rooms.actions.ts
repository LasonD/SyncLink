import { createAction, props } from "@ngrx/store";
import { Room, RoomMember } from "../../models/room.model";
import { Message } from "../../models/message.model";
import { Page } from "../../models/pagination.model";
import { SendMessageData } from "./rooms.effects";

export const getRoom = createAction(
  '[Rooms] Get Room',
  props<{ groupId: number, roomId: number }>()
);

export const getRoomSuccess = createAction(
  '[Rooms] Get Room Success',
  props<{ room: Room }>()
);

export const getMessages = createAction(
  '[Rooms] Get Messages',
  props<{ isPrivate: boolean, groupId: number, roomId: number, otherUserId: number, pageNumber: number, pageSize: number }>()
);

export const getMessagesSuccess = createAction(
  '[Rooms] Get Messages Success',
  props<{ isPrivate: boolean, roomId: number, otherUserId: number, messages: Page<Message> }>()
);

export const getMessagesFailure = createAction(
  '[Rooms] Get Messages Failure',
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
  props<{ name: string, memberIds: number[], description?: string }>()
);

export const createRoomSuccess = createAction(
  '[Rooms] Create Room Success',
  props<{ room: Room }>()
);

export const createRoomFailure = createAction(
  '[Rooms] Create Room Failure',
  props<{ error: any }>()
);

export const sendMessage = createAction(
  '[Rooms] Send Message',
  props<{ senderId: number, isPrivate: boolean, roomId: number, otherUserId: number, payload: SendMessageData, correlationId: string }>()
);

export const sendMessageSuccess = createAction(
  '[Rooms] Send Message Success',
  props<{ isPrivate: boolean, roomId: number, otherUserId: number, message: Message, correlationId: string }>()
);

export const sendMessageFailure = createAction(
  '[Rooms] Send Message Failure',
  props<{ error: any }>()
);

