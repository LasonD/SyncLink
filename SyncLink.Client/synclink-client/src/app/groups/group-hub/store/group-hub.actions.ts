import { createAction, props } from "@ngrx/store";
import { Group, Member } from "../../models/group.model";
import { Page } from "../../models/pagination.model";

export const getGroup = createAction(
  '[Group Hub] Get Group Complete',
  props<{ id: number }>()
);

export const getGroupSuccess = createAction(
  '[Group Hub] Get Group Complete Success',
  props<{ group: Group }>()
);

export const getGroupFailure = createAction(
  '[Group Hub] Get Group Complete Failure',
  props<{ error: any }>()
);

export const getGroupMembers = createAction(
  '[Group Hub] Get Group Members',
  props<{ id: number, pageNumber: number, pageSize: number }>()
);

export const getGroupMembersSuccess = createAction(
  '[Group Hub] Get Group Members Success',
  props<{ membersPage: Page<Member> }>()
);

export const getGroupMembersFailure = createAction(
  '[Group Hub] Get Group Members Failure',
  props<{ error: any }>()
);

