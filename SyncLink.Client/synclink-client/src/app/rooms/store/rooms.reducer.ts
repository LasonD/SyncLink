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
import lodash from 'lodash';
import { SendMessageData } from "./rooms.effects";

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
  on(getMessages, (state): RoomsState => {
    return ({
      ...state,
      messagesLoading: true,
    });
  }),
  on(getMessagesFailure, (state, {error}): RoomsState => ({
    ...state,
    messagesLoading: false,
    messagesError: error,
  })),
  on(getMessagesSuccess, (state, { roomId, messages, otherUserId, isPrivate }): RoomsState => {
    const updatedRoomMessages = lodash.cloneDeep(state.roomMessages);
    const updatedPrivateMessages = lodash.cloneDeep(state.privateMessages);

    const sortedMessages = lodash.sortBy(messages.entities, message => -message.creationDate.getTime());

    if (isPrivate) {
      updatedPrivateMessages[otherUserId] = addNewMessages(updatedPrivateMessages[otherUserId], sortedMessages, messages);
    } else {
      updatedRoomMessages[roomId] = addNewMessages(updatedRoomMessages[roomId], sortedMessages, messages);
    }

    return {
      ...state,
      roomMessages: updatedRoomMessages,
      privateMessages: updatedPrivateMessages,
      messagesLoading: false,
    };
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
  on(sendMessage, (state, { isPrivate, senderId, payload }): RoomsState => {
    const pendingMessage = createPendingMessage(senderId, payload);

    const updatedRoomMessages = lodash.cloneDeep(state.roomMessages);
    const updatedPrivateMessages = lodash.cloneDeep(state.privateMessages);

    if (isPrivate) {
      const userId = payload.recipientId;
      updatedPrivateMessages[userId] = updateMessages(updatedPrivateMessages[userId], pendingMessage);
    } else {
      updatedRoomMessages[payload.roomId] = updateMessages(updatedRoomMessages[payload.roomId], pendingMessage);
    }

    return {
      ...state,
      roomMessages: updatedRoomMessages,
      privateMessages: updatedPrivateMessages,
      pendingMessages: [...state.pendingMessages, pendingMessage]
    };
  }),
  on(sendMessageSuccess, (state, { isPrivate, roomId, otherUserId, message }): RoomsState => {
    const updatedRoomMessages = lodash.cloneDeep(state.roomMessages);
    const updatedPrivateMessages = lodash.cloneDeep(state.privateMessages);

    if (isPrivate) {
      updatedPrivateMessages[otherUserId] = updateMessages(updatedPrivateMessages[otherUserId], message);
    } else {
      updatedRoomMessages[roomId] = updateMessages(updatedRoomMessages[roomId], message);
    }

    return {
      ...state,
      roomMessages: updatedRoomMessages,
      privateMessages: updatedPrivateMessages,
      pendingMessages: removePendingMessage(state.pendingMessages, message)
    };
  }),
);

const updateMessages = (messages, newMessage) => {
  if (!messages) {
    return { messages: [newMessage], lastPage: null };
  }
  return {
    ...messages,
    messages: messages.messages.map(m => m.id === newMessage.id ? newMessage : m)
      .sort((a, b) => b.creationDate.getTime() - a.creationDate.getTime())
  };
}

const removePendingMessage = (pendingMessages, message) => {
  return pendingMessages.filter(m => m.id !== message.id);
}

const addNewMessages = (currentMessages, newMessages, lastPage) => {
  if (!currentMessages) {
    return { messages: [...newMessages], lastPage };
  }
  return {
    messages: [...currentMessages.messages, ...newMessages],
    lastPage
  };
}

const createPendingMessage = (senderId: number, payload: SendMessageData) => {
  return {
    id: Date.now(),
    editedDateTime: null,
    creationDate: new Date(),
    isEdited: false,
    text: payload.text,
    senderId: senderId,
    roomId: payload.roomId,
    groupId: payload.groupId
  };
}





