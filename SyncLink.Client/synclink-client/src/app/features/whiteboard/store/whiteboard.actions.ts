import { createAction, props } from "@ngrx/store";
import { ExtendedWhiteboardElement, Whiteboard } from "./whiteboard.reducer";
import { Page } from "../../../models/pagination.model";

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
  '[Whiteboard] Get Whiteboards Success',
  props<{ whiteboards: Page<Whiteboard> }>()
);

export const getWhiteboardsFailure = createAction(
  '[Whiteboard] Get Whiteboards Failure',
  props<{ error: any }>()
);

export const whiteboardUpdated = createAction(
  '[Whiteboard] Whiteboard Updated',
  props<{ id: number, groupId: number, changes: ExtendedWhiteboardElement[] }>()
);

export const whiteboardUpdatedExternal = createAction(
  '[Whiteboard] Whiteboard Updated External',
  props<{ id: number, groupId: number, changes: ExtendedWhiteboardElement[] }>()
);

export const whiteboardUpdatedSuccess = createAction(
  '[Whiteboard] Whiteboard Updated Success',
  props<{ id: number }>()
);

export const whiteboardUpdatedFailure = createAction(
  '[Whiteboard] Whiteboard Updated Failure',
  props<{ error: any }>()
);

export const createWhiteboard = createAction(
  '[Whiteboard] Create Whiteboard',
  props<{ groupId: number, name: string }>()
);

export const createWhiteboardSuccess = createAction(
  '[Whiteboard] Create Whiteboard Success',
  props<{ whiteboard: Whiteboard }>()
);

export const createWhiteboardFailure = createAction(
  '[Whiteboard] Create Whiteboard Failure',
  props<{ error: any }>()
);


