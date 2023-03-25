import { createAction, props } from '@ngrx/store';
import { Group, GroupSearchMode } from '../models/group.model';

export const searchGroups = createAction(
  '[Group Search] Search Groups',
  props<{ searchQuery: string; groupSearchMode: GroupSearchMode }>()
);

export const searchGroupsSuccess = createAction(
  '[Group Search] Search Groups Success',
  props<{ groups: Group[] }>()
);

export const searchGroupsFailure = createAction(
  '[Group Search] Search Groups Failure',
  props<{ error: any }>()
);

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

export interface CreateGroupDto {
  name: string,
  description: string,
}
