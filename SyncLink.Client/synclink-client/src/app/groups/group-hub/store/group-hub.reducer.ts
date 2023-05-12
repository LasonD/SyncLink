import { Group, GroupMember } from "../../../models/group.model";
import { createReducer, on } from "@ngrx/store";
import {
  getGroup,
  getGroupFailure,
  getGroupMembersFailure,
  getGroupMembersSuccess,
  getGroupSuccess
} from "./group-hub.actions";
import { Page } from "../../../models/pagination.model";

export interface GroupHubState {
  group: Group;
  groupLoading: boolean;
  groupError: any;

  groupMembers: Page<GroupMember>[];
  groupMembersLoading: boolean,
  groupMembersError: any,
}

export const initialState: GroupHubState = {
  group: null,
  groupLoading: false,
  groupError: null,

  groupMembers: [],
  groupMembersLoading: false,
  groupMembersError: null,
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
);
