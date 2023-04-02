import { combineReducers } from "@ngrx/store";
import { groupsSearchReducer, GroupsSearchState } from "../groups-search/store/groups-search.reducer";
import { createGroupReducer, CreateGroupState } from "../create-group/store/create-group.reducer";

export interface GroupsState {
  groupsSearch: GroupsSearchState,
  createGroup: CreateGroupState,
}

export const groupsReducer = combineReducers<GroupsState>({
  groupsSearch: groupsSearchReducer,
  createGroup: createGroupReducer,
});
