import { createAction, props } from '@ngrx/store';
import { GroupOverview, GroupSearchMode } from "../../models/group.model";

export const searchGroups = createAction(
  '[Group Search] Search Groups',
  props<{ searchQuery: string; groupSearchMode: GroupSearchMode }>()
);

export const searchGroupsSuccess = createAction(
  '[Group Search] Search Groups Success',
  props<{ groups: GroupOverview[] }>()
);

export const searchGroupsFailure = createAction(
  '[Group Search] Search Groups Failure',
  props<{ error: any }>()
);
