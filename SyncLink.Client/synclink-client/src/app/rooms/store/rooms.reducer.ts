import { Group, GroupMember, Room } from "../../models/group.model";
import { Page } from "../../models/pagination.model";
import { createReducer, on } from "@ngrx/store";
import {
  getGroup,
  getGroupFailure, getGroupMembersFailure,
  getGroupMembersSuccess,
  getGroupSuccess
} from "../../groups/group-hub/store/group-hub.actions";

export interface RoomsState {
  room: Room;
  roomLoading: boolean;
  roomError: any;

  groupMembers: Page<GroupMember>[];
  groupMembersLoading: boolean,
  groupMembersError: any,
}

export const initialState: RoomsState = {
  room: null,
  roomLoading: false,
  roomError: null,

  groupMembers: [],
  groupMembersLoading: false,
  groupMembersError: null,
};

// export const groupHubReducer = createReducer(
//   initialState,
//   on(getGroup, (state) : GroupHubState => ({ ...state, groupLoading: true })),
//   on(getGroupSuccess, (state, { group }) : GroupHubState => ({...state, group, groupLoading: false })),
//   on(getGroupFailure, (state, { error }) : GroupHubState => ({ ...state, groupError: error, groupLoading: false })),
//
//   on(getGroupMembersSuccess, (state, { membersPage }) : GroupHubState => {
//     return ({...state, groupMembersError: null, groupMembersLoading: false, groupMembers: [membersPage, ...state.groupMembers]});
//   }),
//   on(getGroupMembersFailure, (state, { error }) : GroupHubState => {
//     return ({...state, groupMembersError: error, groupMembersLoading: false});
//   }),
// );
