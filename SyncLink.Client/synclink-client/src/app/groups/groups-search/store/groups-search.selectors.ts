import { createSelector } from '@ngrx/store';
import { GroupsSearchState } from "./groups-search.reducer";
import { selectGroupsFeature } from "../../group-hub/store/group-hub.selectors";

export const selectGroupSearchFeature = createSelector(
  selectGroupsFeature,
  state => state.groupsSearch
);

export const selectGroupsSearchGroups = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.searchedGroups.flatMap((p) => p.entities)
);

export const selectGroupSearchLoading = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.searchGroupLoading
);

export const selectGroupsSearchError = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.groupSearchError
);
