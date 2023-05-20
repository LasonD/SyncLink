import { createReducer, on } from "@ngrx/store";
import {
  createWhiteboard, createWhiteboardFailure, createWhiteboardSuccess,
  getWhiteboard,
  getWhiteboardFailure,
  getWhiteboards, getWhiteboardsFailure,
  getWhiteboardsSuccess,
  getWhiteboardSuccess
} from "./whiteboard.actions";
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
  createdWhiteboardId: number | null;
  whiteboardLoading: boolean;
  whiteboardError: any;
}

export const adapter: EntityAdapter<Whiteboard> = createEntityAdapter<Whiteboard>();

export const initialState: WhiteboardState = adapter.getInitialState({
  selectedWhiteboardId: null,
  createdWhiteboardId: null,
  whiteboardError: null,
  whiteboardLoading: false,
});

export const whiteboardReducer = createReducer(
  initialState,
  on(getWhiteboard, (state): WhiteboardState => ({ ...state, whiteboardLoading: true })),
  on(getWhiteboards, (state): WhiteboardState => ({...state, whiteboardLoading: true})),
  on(getWhiteboardSuccess, (state, { whiteboard }): WhiteboardState => adapter.upsertOne(whiteboard, {
    ...state,
    selectedWhiteboardId: whiteboard.id,
    whiteboardLoading: false
  })),
  on(getWhiteboardsSuccess, (state, { whiteboards }): WhiteboardState => adapter.addMany(whiteboards.entities, { ...state, whiteboardLoading: false })),
  on(getWhiteboardFailure, (state, { error }): WhiteboardState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
  on(getWhiteboardsFailure, (state, { error }): WhiteboardState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
  on(createWhiteboard, (state): WhiteboardState => ({...state, whiteboardLoading: true})),
  on(createWhiteboardSuccess, (state, { whiteboard }): WhiteboardState => adapter.upsertOne(whiteboard, {
    ...state,
    createdWhiteboardId: whiteboard.id,
    whiteboardLoading: false
  })),
  on(createWhiteboardFailure, (state, { error }): WhiteboardState => ({ ...state, whiteboardError: error, whiteboardLoading: false })),
);
