import { createAction, props } from "@ngrx/store";
import { Whiteboard, WhiteboardOverview } from "./whiteboard.reducer";
import { WhiteboardElement } from "ng-whiteboard";

export const getWhiteboard = createAction(
  '[Whiteboard] Get Whiteboard',
  props<{ id: number, groupId: number }>()
);

export const getWhiteboardSuccess = createAction(
  '[Whiteboard] Get Whiteboard Success',
  props<{ whiteboard: Whiteboard }>()
);

export const getWhiteboardFailure = createAction(
  '[Whiteboard] Get Whiteboard Failure',
  props<{ error: any }>()
);

export const getWhiteboards= createAction(
  '[Whiteboard] Get Whiteboards',
  props<{ groupId: number }>()
);

export const getWhiteboardsSuccess = createAction(
  '[Whiteboard] Get Whiteboard Success',
  props<{ whiteboards: Whiteboard[] }>()
);

export const getWhiteboardsFailure = createAction(
  '[Whiteboard] Get Whiteboard Failure',
  props<{ error: any }>()
);

export const whiteboardUpdated = createAction(
  '[Whiteboard] Whiteboard Updated',
  props<{ id: number, groupId: number, changes: WhiteboardElement[] }>()
);

export const whiteboardUpdatedSuccess = createAction(
  '[Whiteboard] Whiteboard Updated Success',
  props<{ id: number }>()
);

export const whiteboardUpdatedFailure = createAction(
  '[Whiteboard] Whiteboard Updated Failure',
  props<{ error: any }>()
);
