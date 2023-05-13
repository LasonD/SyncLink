import { Page } from "../../models/pagination.model";
import { Room, RoomMember } from "../../models/room.model";
import { Message } from "src/app/models/message.model";
import { createReducer, on } from "@ngrx/store";
import {
  getRoom,
  getRoomFailure, getRoomMembers, getRoomMembersFailure, getRoomMembersSuccess,
  getRoomMessages,
  getRoomMessagesFailure,
  getRoomMessagesSuccess,
  getRoomSuccess
} from "./rooms.actions";

export interface RoomsState {
  rooms: { otherUserId: number, room: Room }[],
  roomLoading: boolean;
  roomError: any;

  roomMembers: { roomId: number, members: Page<RoomMember> }[];
  roomMembersLoading: boolean,
  roomMembersError: any,

  roomMessages: { [roomId: number]: { messages: Message[], lastPage: Page<Message> }};
  roomMessagesLoading: boolean,
  roomMessagesError: any,
}

export const initialState: RoomsState = {
  rooms: [],
  roomMembers: [],
  roomMembersError: null,
  roomMembersLoading: false,
  roomMessages: [],
  roomMessagesError: null,
  roomMessagesLoading: false,
  roomLoading: false,
  roomError: null,
};

export const roomsReducer = createReducer(
  initialState,
  on(getRoom, (state) : RoomsState => ({ ...state, roomLoading: true })),
  on(getRoomSuccess, (state, { otherUserId, room }) : RoomsState => ({...state, rooms: [...state.rooms, { otherUserId: otherUserId, room: room }], roomLoading: false })),
  on(getRoomFailure, (state, { error }) : RoomsState => ({ ...state, roomError: error })),
  on(getRoomMessages, (state) : RoomsState => ({
    ...state,
    roomMessagesLoading: true,
  })),
  on(getRoomMessagesFailure, (state, { error }) : RoomsState => ({
      ...state,
      roomMessagesLoading: false,
      roomMessagesError: error,
    })),
  on(getRoomMessagesSuccess, (state, { roomId, messages }) : RoomsState => {
    let updatedRoomMessages = { ...state.roomMessages };

    if (!updatedRoomMessages[roomId]) {
      updatedRoomMessages[roomId] = { messages: [], lastPage: null };
    }

    const newMessages = [
      ...updatedRoomMessages[roomId].messages,
      ...messages.entities
    ];

    const sortedMessages = newMessages.sort((a: Message, b: Message) =>
      b.creationDate.getTime() - a.creationDate.getTime()
    );

    return ({
      ...state,
      roomMessages: {
        ...updatedRoomMessages,
        [roomId]: {
          messages: sortedMessages,
          lastPage: messages
        }
      },
      roomMessagesLoading: false,
    });
  }),
on(getRoomMembers, (state) : RoomsState => ({
    ...state,
    roomMembersLoading: true,
  })),
  on(getRoomMembersFailure, (state, { error }) : RoomsState => ({
    ...state,
    roomMembersLoading: false,
    roomMembersError: error,
  })),
  on(getRoomMembersSuccess, (state, { roomId, members }) : RoomsState => ({
    ...state,
    roomMembers: [...state.roomMembers, { roomId: roomId, members: members }],
    roomMembersLoading: false,
  })),
);
