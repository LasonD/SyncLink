import { createFeatureSelector, createSelector } from "@ngrx/store";
import { RoomsState } from "./rooms.reducer";
import { Message } from "../../models/message.model";
import { Page } from "../../models/pagination.model";

export const selectRoomsFeature = createFeatureSelector<RoomsState>('rooms');

export const selectRooms = createSelector(
  selectRoomsFeature,
  (state: RoomsState) => state.rooms
);

export const selectRoomMessages = createSelector(
  selectRoomsFeature,
  (state: RoomsState): { [roomId: number]: { messages: Message[], lastPage: Page<Message> } } => state.roomMessages
);

export const selectPrivateMessages = createSelector(
  selectRoomsFeature,
  (state: RoomsState): { [roomId: number]: { messages: Message[], lastPage: Page<Message> } } => state.privateMessages
);

export const selectRoomError = createSelector(
  selectRoomsFeature,
  (state: RoomsState) => state.roomError
);

export const selectRoomMembers = createSelector(
  selectRoomsFeature,
  (state: RoomsState) => state.roomMembers
);

export const selectRoomMessagesError = createSelector(
  selectRoomsFeature,
  (state: RoomsState) => state.messagesError
);


