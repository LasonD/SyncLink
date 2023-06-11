import { createAction, props } from '@ngrx/store';
import { Group, GroupSearchMode } from "../../../models/group.model";
import { Page } from "../../../models/pagination.model";
import { GroupJoinRequestState, SendGroupJoinRequest } from "./groups-search.reducer";

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

export const sendGroupJoinRequest = createAction(
  '[Group Search] Send Group Join Request',
  props<{ groupId: number, request: SendGroupJoinRequest }>()
)

export const sendGroupJoinRequestSuccess = createAction(
  '[Group Search] Send Group Join Request Success',
  props<{ joinRequestState: GroupJoinRequestState }>()
)

export const sendGroupJoinRequestFailure = createAction(
  '[Group Search] Send Group Join Request Failure',
  props<{ error: any }>()
)
