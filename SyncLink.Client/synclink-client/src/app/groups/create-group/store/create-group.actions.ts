import { createAction, props } from "@ngrx/store";
import { CreateGroupDto, Group } from "../../../models/group.model";

export const createGroup = createAction(
  '[Group Create] Create Group',
  props<CreateGroupDto>()
);

export const createGroupSuccess = createAction(
  '[Group Create] Create Group Success',
  props<{ group: Group }>()
);

export const createGroupError = createAction(
  '[Group Create] Create Group Error',
  props<{ error: any }>()
);
