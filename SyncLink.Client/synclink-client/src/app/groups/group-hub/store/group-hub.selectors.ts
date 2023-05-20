import { createFeatureSelector, createSelector } from "@ngrx/store";
import { GroupHubState } from "./group-hub.reducer";
import lodash from "lodash";
import { GroupMember } from "../../../models/group.model";
import { GroupsState } from "../../store/groups.reducer";

export const selectGroupsFeature = createFeatureSelector<GroupsState>('groups');

export const selectGroupHubState = createSelector(
  selectGroupsFeature,
  (state: GroupsState) => state.groupHub
)

export const selectGroupHubGroup = createSelector(
  selectGroupHubState,
  (state: GroupHubState) => state.group
);

export const selectGroupHubLoading = createSelector(
  selectGroupHubState,
  (state: GroupHubState) => state.groupLoading
);

export const selectGroupHubError = createSelector(
  selectGroupHubState,
  (state: GroupHubState) => state.groupError
);

export const selectGroupMemberPages = createSelector(
  selectGroupHubState,
  (state: GroupHubState) => state.groupMembers
);

export const selectGroupMembers = createSelector(
  selectGroupHubState,
  (state: GroupHubState) => lodash.uniqBy(state.groupMembers.flatMap(p => p.entities), 'id') as GroupMember[]
);

export const selectGroupHubRooms = createSelector(
  selectGroupHubState,
  (state: GroupHubState) => state.groupRooms
);

export const selectCurrentGroupId = createSelector(
  selectGroupHubState,
  (state: GroupHubState) => state.group?.id
);
