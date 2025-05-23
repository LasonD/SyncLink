import { createAction, props } from "@ngrx/store";
import { Group, GroupMember } from "../../../models/group.model";
import { Page } from "../../../models/pagination.model";
import { Room } from "../../../models/room.model";

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

export const getGroupRooms = createAction(
  '[Group Hub] Get Group Rooms',
  props<{ id: number, pageNumber: number, pageSize: number }>()
);

export const getGroupRoomsSuccess = createAction(
  '[Group Hub] Get Group Rooms Success',
  props<{ roomsPage: Page<Room> }>()
);

export const getGroupRoomsFailure = createAction(
  '[Group Hub] Get Group Rooms Failure',
  props<{ error: any }>()
);

export const getGroupMembersSuccess = createAction(
  '[Group Hub] Get Group Members Success',
  props<{ membersPage: Page<GroupMember> }>()
);

export const getGroupMembersFailure = createAction(
  '[Group Hub] Get Group Members Failure',
  props<{ error: any }>()
);

export const openGroup = createAction(
  '[Group Hub] Open Group',
  props<{ groupId: number }>()
);

export const closeGroup = createAction(
  '[Group Hub] Close Group',
  props<{ groupId: number }>()
);

