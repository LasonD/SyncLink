import { createSelector } from '@ngrx/store';
import { adapter, GroupsSearchState } from "./groups-search.reducer";
import { selectGroupsFeature } from "../../group-hub/store/group-hub.selectors";
import { AppState } from "../../../store/app.reducer";

const groupsSelectors = adapter.getSelectors((state: AppState) => state.groups.groupsSearch);

export const selectGroupSearchFeature = createSelector(
  selectGroupsFeature,
  state => state.groupsSearch
);

export const selectGroupsSearchGroups = createSelector(
  groupsSelectors.selectAll,
  (groups) => {
    return groups ?? [];
  }
);

export const selectGroupSearchLoading = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.searchGroupLoading
);

export const selectGroupsSearchError = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.groupSearchError
);
