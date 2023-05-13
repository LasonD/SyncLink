import { createFeatureSelector, createSelector } from "@ngrx/store";
import { RoomsState } from "./rooms.reducer";
import { Message } from "../../models/message.model";

export const selectRoomsFeature = createFeatureSelector<RoomsState>('rooms');

export const selectRooms = createSelector(
  selectRoomsFeature,
  (state: RoomsState) => state.rooms
);

export const selectRoomMessages = createSelector(
  selectRoomsFeature,
  (state: RoomsState): { [roomId: number]: Message[] } => {
    const roomMessages = state.roomMessages;
    const messagesByRoom = {};

    roomMessages.forEach(roomMessage => {
      const roomId = roomMessage.roomId;
      const messages = roomMessage.messages.entities;

      const sortedMessages = messages.sort((a: Message, b: Message) =>
        b.creationDate.getTime() - a.creationDate.getTime()
      );

      messagesByRoom[roomId] = sortedMessages;
    });

    return messagesByRoom;
  }
);


