import { Group, GroupMember } from "../../../models/group.model";
import { createReducer, on } from "@ngrx/store";
import {
  getGroup,
  getGroupFailure,
  getGroupMembersFailure,
  getGroupMembersSuccess, getGroupRoomsFailure, getGroupRoomsSuccess,
  getGroupSuccess
} from "./group-hub.actions";
import { Page } from "../../../models/pagination.model";
import { Room } from "../../../models/room.model";

export interface GroupHubState {
  group: Group;
  groupLoading: boolean;
  groupError: any;

  groupMembers: Page<GroupMember>[];
  groupRooms: Page<Room>[];

  groupMembersLoading: boolean,
  groupMembersError: any,

  groupRoomsLoading: boolean,
  groupRoomsError: any,
}

export const initialState: GroupHubState = {
  group: null,
  groupLoading: false,
  groupError: null,

  groupMembers: [],
  groupRooms: [],

  groupMembersLoading: false,
  groupMembersError: null,

  groupRoomsLoading: false,
  groupRoomsError: null,
};

export const groupHubReducer = createReducer(
  initialState,
  on(getGroup, (state) : GroupHubState => ({ ...state, groupLoading: true })),
  on(getGroupSuccess, (state, { group }) : GroupHubState => ({...state, group, groupLoading: false })),
  on(getGroupFailure, (state, { error }) : GroupHubState => ({ ...state, groupError: error, groupLoading: false })),

  on(getGroupMembersSuccess, (state, { membersPage }) : GroupHubState => {
    return ({...state, groupMembersError: null, groupMembersLoading: false, groupMembers: [membersPage, ...state.groupMembers]});
  }),
  on(getGroupMembersFailure, (state, { error }) : GroupHubState => {
    return ({...state, groupMembersError: error, groupMembersLoading: false});
  }),

  on(getGroupRoomsSuccess, (state, { roomsPage }) : GroupHubState => {
    return ({...state, groupRoomsError: null, groupRoomsLoading: false, groupRooms: [roomsPage, ...state.groupRooms]});
  }),
  on(getGroupRoomsFailure, (state, { error }) : GroupHubState => {
    return ({...state, groupRoomsError: error, groupRoomsLoading: false});
  }),
);
