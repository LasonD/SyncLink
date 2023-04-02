import { createFeatureSelector, createSelector } from '@ngrx/store';
import { GroupsSearchState } from "./groups-search.reducer";

export const selectGroupSearchFeature = createFeatureSelector<GroupsSearchState>('groupSearch');

export const selectGroupsSearchGroups = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.searchedGroups
);

export const selectGroupSearchLoading = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.searchGroupLoading
);

export const selectGroupsSearchError = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.groupSearchError
);
