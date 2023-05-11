import { createFeatureSelector, createSelector } from "@ngrx/store";
import { GroupHubState } from "./group-hub.reducer";

export const selectGroupHubFeature = createFeatureSelector<GroupHubState>('groupHub');

export const selectGroupHubGroup = createSelector(
  selectGroupHubFeature,
  (state: GroupHubState) => state.group
);

export const selectGroupHubLoading = createSelector(
  selectGroupHubFeature,
  (state: GroupHubState) => state.groupLoading
);

export const selectGroupHubError = createSelector(
  selectGroupHubFeature,
  (state: GroupHubState) => state.groupError
);

export const selectGroupHubMembers = createSelector(
  selectGroupHubFeature,
  (state: GroupHubState) => state.groupMembers
);
