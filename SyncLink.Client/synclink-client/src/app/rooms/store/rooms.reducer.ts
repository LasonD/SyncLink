import { Page } from "../../models/pagination.model";
import { Room, RoomMember } from "../../models/room.model";
import { Message } from "src/app/models/message.model";
import { createReducer, on } from "@ngrx/store";
import {
  getRoom,
  getRoomFailure, getRoomMembers, getRoomMembersFailure, getRoomMembersSuccess,
  getMessages,
  getMessagesFailure,
  getMessagesSuccess,
  getRoomSuccess, sendMessage, sendMessageSuccess
} from "./rooms.actions";

export interface RoomsState {
  rooms: Room[],
  roomLoading: boolean;
  roomError: any;

  roomMembers: { roomId: number, members: Page<RoomMember> }[];
  roomMembersLoading: boolean,
  roomMembersError: any,

  roomMessages: { [roomId: number]: { messages: Message[], lastPage: Page<Message> }};
  privateMessages: { [otherUserId: number]: { messages: Message[], lastPage: Page<Message> }};

  messagesLoading: boolean,
  messagesError: any,

  pendingMessages: Message[],
  sendMessageError: any,
}

export const initialState: RoomsState = {
  rooms: [],
  roomMembers: [],
  roomMembersError: null,
  roomMembersLoading: false,
  roomMessages: [],
  privateMessages: [],
  messagesError: null,
  messagesLoading: false,
  roomLoading: false,
  roomError: null,
  sendMessageError: null,
  pendingMessages: [],
};

export const roomsReducer = createReducer(
  initialState,
  on(getRoom, (state): RoomsState => ({...state, roomLoading: true})),
  on(getRoomSuccess, (state, {room}): RoomsState => ({
    ...state,
    rooms: [...state.rooms, room],
    roomLoading: false
  })),
  on(getRoomFailure, (state, {error}): RoomsState => ({...state, roomError: error})),
  on(getMessages, (state): RoomsState => ({
    ...state,
    messagesLoading: true,
  })),
  on(getMessagesFailure, (state, {error}): RoomsState => ({
    ...state,
    messagesLoading: false,
    messagesError: error,
  })),
  on(getMessagesSuccess, (state, { roomId, messages, otherUserId, isPrivate }): RoomsState => {
    let updatedRoomMessages = { ...state.roomMessages };
    let updatedPrivateMessages = { ...state.privateMessages };

    const newMessages = messages.entities;

    const sortedMessages = newMessages.sort((a: Message, b: Message) =>
      b.creationDate.getTime() - a.creationDate.getTime()
    );

    if (isPrivate) {
      if (!updatedPrivateMessages[otherUserId]) {
        updatedPrivateMessages[otherUserId] = { messages: [], lastPage: null };
      }

      updatedPrivateMessages[otherUserId] = {
        messages: [...updatedPrivateMessages[otherUserId].messages, ...sortedMessages],
        lastPage: messages
      };

    } else {
      if (!updatedRoomMessages[roomId]) {
        updatedRoomMessages[roomId] = { messages: [], lastPage: null };
      }

      updatedRoomMessages[roomId] = {
        messages: [...updatedRoomMessages[roomId].messages, ...sortedMessages],
        lastPage: messages
      };
    }

    return ({
      ...state,
      roomMessages: updatedRoomMessages,
      privateMessages: updatedPrivateMessages,
      messagesLoading: false,
    });
  }),
  on(getRoomMembers, (state): RoomsState => ({
    ...state,
    roomMembersLoading: true,
  })),
  on(getRoomMembersFailure, (state, {error}): RoomsState => ({
    ...state,
    roomMembersLoading: false,
    roomMembersError: error,
  })),
  on(getRoomMembersSuccess, (state, {roomId, members}): RoomsState => ({
    ...state,
    roomMembers: [...state.roomMembers, {roomId: roomId, members: members}],
    roomMembersLoading: false,
  })),
  on(sendMessage, (state, { isPrivate, payload }): RoomsState => {
    const pendingMessage: Message = {
      id: Date.now(), // create a temporary id for this pending message
      editedDateTime: null,
      creationDate: new Date(),
      isEdited: false,
      text: payload.text,
      senderId: payload.recipientId,  // I'm assuming that senderId is the recipientId in this case
      roomId: payload.roomId,
      groupId: payload.groupId
    };

    let updatedRoomMessages = { ...state.roomMessages };
    let updatedPrivateMessages = { ...state.privateMessages };

    if (isPrivate) {
      const userId = payload.recipientId;
      if (!updatedPrivateMessages[userId]) {
        updatedPrivateMessages[userId] = { messages: [], lastPage: null };
      }
      updatedPrivateMessages[userId].messages = [...updatedPrivateMessages[userId].messages, pendingMessage];
    } else {
      if (!updatedRoomMessages[payload.roomId]) {
        updatedRoomMessages[payload.roomId] = { messages: [], lastPage: null };
      }
      updatedRoomMessages[payload.roomId].messages = [...updatedRoomMessages[payload.roomId].messages, pendingMessage];
    }

    return {
      ...state,
      roomMessages: updatedRoomMessages,
      privateMessages: updatedPrivateMessages,
      pendingMessages: [...state.pendingMessages, pendingMessage]
    };
  }),
  on(sendMessageSuccess, (state, { isPrivate, roomId, otherUserId, message }): RoomsState => {
    let updatedRoomMessages = { ...state.roomMessages };
    let updatedPrivateMessages = { ...state.privateMessages };
    let updatedPendingMessages = state.pendingMessages.filter(m => m.id !== message.id);

    if (isPrivate) {
      if (!updatedPrivateMessages[otherUserId]) {
        updatedPrivateMessages[otherUserId] = { messages: [], lastPage: null };
      }
      updatedPrivateMessages[otherUserId].messages = updatedPrivateMessages[otherUserId].messages.map(m => m.id === message.id ? message : m);
      updatedPrivateMessages[otherUserId].messages.sort((a, b) => b.creationDate.getTime() - a.creationDate.getTime());
    } else {
      if (!updatedRoomMessages[roomId]) {
        updatedRoomMessages[roomId] = { messages: [], lastPage: null };
      }
      updatedRoomMessages[roomId].messages = updatedRoomMessages[roomId].messages.map(m => m.id === message.id ? message : m);
      updatedRoomMessages[roomId].messages.sort((a, b) => b.creationDate.getTime() - a.creationDate.getTime());
    }

    return {
      ...state,
      roomMessages: updatedRoomMessages,
      privateMessages: updatedPrivateMessages,
      pendingMessages: updatedPendingMessages
    };
  }),
);




