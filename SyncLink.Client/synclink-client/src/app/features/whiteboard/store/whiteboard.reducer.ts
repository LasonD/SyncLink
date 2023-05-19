import { createReducer, on } from "@ngrx/store";
import { getWhiteboard, getWhiteboardFailure, getWhiteboardSuccess } from "./whiteboard.actions";
import { WhiteboardElement } from "ng-whiteboard";
import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";

export interface Whiteboard {
  id: number,
  name: string,
  whiteboardElements: WhiteboardElement[],
  creatorId: number,
  groupId: number,
}

export interface WhiteboardState extends EntityState<Whiteboard> {
  selectedWhiteboardId: number | null;
  whiteboardLoading: boolean;
  whiteboardError: any;
}

export const adapter: EntityAdapter<Whiteboard> = createEntityAdapter<Whiteboard>();

export const initialState: WhiteboardState = adapter.getInitialState({
  selectedWhiteboardId: null,
  whiteboardError: null,
  whiteboardLoading: false,
});

export const whiteboardReducer = createReducer(
  initialState,
  on(getWhiteboard, (state): WhiteboardState => ({ ...state, whiteboardLoading: true })),
  on(getWhiteboardSuccess, (state, { whiteboard }): WhiteboardState => adapter.upsertOne(whiteboard, { ...state, selectedWhiteboardId: whiteboard.id, whiteboardLoading: false })),
  on(getWhiteboardFailure, (state, { error }): WhiteboardState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
);
