import { createAction, props } from "@ngrx/store";
import { Group } from "../../models/group.model";

export const getGroupComplete = createAction(
  '[Group Hub] Get Group Complete',
  props<{ id: string }>()
);

export const getGroupCompleteSuccess = createAction(
  '[Group Hub] Get Group Complete Success',
  props<{ group: Group }>()
);

export const getGroupCompleteFailure = createAction(
  '[Group Hub] Get Group Complete Failure',
  props<{ error: any }>()
);
