import { createReducer, on } from '@ngrx/store';
import { Group } from "../../../models/group.model";
import { searchGroups, searchGroupsFailure, searchGroupsSuccess } from "./groups-search.actions";
import { Page } from "../../../models/pagination.model";

export interface GroupsSearchState {
  searchedGroups: Page<Group>[];
  searchGroupLoading: boolean;
  groupSearchError: any;
}

export const initialState: GroupsSearchState = {
  searchedGroups: [],
  searchGroupLoading: false,
  groupSearchError: null,
};

export const groupsSearchReducer = createReducer(
  initialState,
  on(searchGroups, (state): GroupsSearchState => ({ ...state, searchGroupLoading: true })),
  on(searchGroupsSuccess, (state, { groups }): GroupsSearchState => ({...state, searchGroupLoading: false, searchedGroups: [...state.searchedGroups, groups]})),
  on(searchGroupsFailure, (state, { error }): GroupsSearchState => ({ ...state, searchGroupLoading: false, groupSearchError: error })),
);
