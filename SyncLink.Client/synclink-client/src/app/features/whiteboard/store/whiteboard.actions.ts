import { createAction, props } from "@ngrx/store";
import { Whiteboard } from "./whiteboard.reducer";
import { WhiteboardElement } from "ng-whiteboard";

export const getWhiteboard = createAction(
  '[Whiteboard] Get Whiteboard',
  props<{ id: number }>()
);

export const getWhiteboardSuccess = createAction(
  '[Whiteboard] Get Whiteboard Success',
  props<{ whiteboard: Whiteboard }>()
);

export const getWhiteboardFailure = createAction(
  '[Whiteboard] Get Whiteboard Failure',
  props<{ error: any }>()
);

export const whiteboardChanged = createAction(
  '[Whiteboard] Whiteboard Changed',
  props<{ id: number, changes: WhiteboardElement[] }>()
);
