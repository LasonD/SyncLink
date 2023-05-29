import { createSelector } from '@ngrx/store';
import { GroupsSearchState } from "./groups-search.reducer";
import { selectGroupsFeature } from "../../group-hub/store/group-hub.selectors";
import * as _ from "lodash";
import { Group } from "../../../models/group.model";

export const selectGroupSearchFeature = createSelector(
  selectGroupsFeature,
  state => state.groupsSearch
);

export const selectGroupsSearchGroups = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => _.uniqBy(state.searchedGroups.flatMap((p) => p.entities), 'id') as Group[]
);

export const selectGroupSearchLoading = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.searchGroupLoading
);

export const selectGroupsSearchError = createSelector(
  selectGroupSearchFeature,
  (state: GroupsSearchState) => state.groupSearchError
);
