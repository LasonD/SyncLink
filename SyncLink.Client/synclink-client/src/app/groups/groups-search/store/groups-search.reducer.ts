import { createReducer, on } from '@ngrx/store';
import { Group } from "../../models/group.model";
import { searchGroups, searchGroupsFailure, searchGroupsSuccess } from "./groups-search.actions";

export interface GroupsSearchState {
  searchedGroups: Group[];
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
  on(searchGroups, (state) => ({ ...state, loading: true })),
  on(searchGroupsSuccess, (state, { groups }) => ({...state, searchGroupLoading: false, searchedGroups: groups})),
  on(searchGroupsFailure, (state, { error }) => ({ ...state, searchGroupLoading: false, error })),
);
