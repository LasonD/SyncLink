import { createAction, props } from '@ngrx/store';
import { Group, GroupSearchMode } from "../../models/group.model";
import { Page } from "../../models/pagination.model";

export const searchGroups = createAction(
  '[Group Search] Search Groups',
  props<{ searchQuery: string; groupSearchMode: GroupSearchMode }>()
);

export const searchGroupsSuccess = createAction(
  '[Group Search] Search Groups Success',
  props<{ groups: Page<Group> }>()
);

export const searchGroupsFailure = createAction(
  '[Group Search] Search Groups Failure',
  props<{ error: any }>()
);
