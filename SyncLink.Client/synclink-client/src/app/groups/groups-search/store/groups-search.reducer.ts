import { createReducer, on } from '@ngrx/store';
import { Group } from "../../../models/group.model";
import { searchGroups, searchGroupsFailure, searchGroupsSuccess } from "./groups-search.actions";
import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";

export const groupsAdapter: EntityAdapter<Group> = createEntityAdapter<Group>();

export interface GroupsSearchState extends EntityState<Group> {
  searchGroupLoading: boolean;
  groupSearchError: any;
}

export const initialState: GroupsSearchState = groupsAdapter.getInitialState({
  searchGroupLoading: false,
  groupSearchError: null,
});

export const adapter: EntityAdapter<Group> = createEntityAdapter<Group>();

export const groupsSearchReducer = createReducer(
  initialState,
  on(searchGroups, (state): GroupsSearchState => {
    return ({...state, searchGroupLoading: true});
  }),
  on(searchGroupsSuccess, (state, { groups }): GroupsSearchState => {
    return adapter.upsertMany(groups.entities, adapter.removeAll({...state, searchGroupLoading: false}));
  }),
  on(searchGroupsFailure, (state, { error }): GroupsSearchState => {
    return adapter.removeAll({...state, searchGroupLoading: false, groupSearchError: error});
  }),
);

export interface SendGroupJoinRequest {
  message: string,
}

export interface GroupJoinRequestState {
  message: string,
  userId: number,
  groupId: number,
  status: GroupJoinRequestStatus,
}

export enum GroupJoinRequestStatus {
  Pending = 0,
  Accepted = 1,
  Rejected = 2,
}
