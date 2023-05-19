import { createReducer, on } from "@ngrx/store";
import { getWhiteboard, getWhiteboardFailure, getWhiteboardSuccess } from "./whiteboard.actions";
import { WhiteboardElement } from "ng-whiteboard";

export interface WhiteboardState {
  whiteboard: Whiteboard;
  whiteboardLoading: boolean;
  whiteboardError: any;
}

export const initialState: WhiteboardState = {
  whiteboard: null,
  whiteboardError: null,
  whiteboardLoading: false,
};

export const whiteboardReducer = createReducer(
  initialState,
  on(getWhiteboard, (state) : WhiteboardState => ({ ...state, whiteboardLoading: true })),
  on(getWhiteboardSuccess, (state, { whiteboard }) : WhiteboardState => ({ ...state, whiteboard, whiteboardLoading: false })),
  on(getWhiteboardFailure, (state, { error }) : WhiteboardState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
);

export interface Whiteboard {
  id: number,
  name: string,
  whiteboardElements: WhiteboardElement[],
  creatorId: number,
  groupId: number,
}
