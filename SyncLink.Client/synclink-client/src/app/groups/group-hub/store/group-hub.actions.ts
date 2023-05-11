import { createAction, props } from "@ngrx/store";
import { Group } from "../../models/group.model";

export const getGroup = createAction(
  '[Group Hub] Get Group Complete',
  props<{ id: string }>()
);

export const getGroupSuccess = createAction(
  '[Group Hub] Get Group Complete Success',
  props<{ group: Group }>()
);

export const getGroupFailure = createAction(
  '[Group Hub] Get Group Complete Failure',
  props<{ error: any }>()
);
