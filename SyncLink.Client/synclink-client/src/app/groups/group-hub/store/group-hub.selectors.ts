import { createFeatureSelector, createSelector } from "@ngrx/store";
import { GroupHubState } from "./group-hub.reducer";
import lodash from "lodash";
import { GroupMember } from "../../../models/group.model";

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

export const selectGroupMemberPages = createSelector(
  selectGroupHubFeature,
  (state: GroupHubState) => state.groupMembers
);

export const selectGroupMembers = createSelector(
  selectGroupHubFeature,
  (state: GroupHubState) => lodash.uniqBy(state.groupMembers.flatMap(p => p.entities), 'id') as GroupMember[]
);

export const selectGroupHubRooms = createSelector(
  selectGroupHubFeature,
  (state: GroupHubState) => state.groupRooms
);
