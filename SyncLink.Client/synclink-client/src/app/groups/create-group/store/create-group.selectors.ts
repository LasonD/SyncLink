import { createSelector } from "@ngrx/store";
import { CreateGroupState } from "./create-group.reducer";
import { selectGroupsFeature } from "../../group-hub/store/group-hub.selectors";

export const selectCreateGroupFeature = createSelector(
  selectGroupsFeature,
  state => state.createGroup,
);

export const selectCreatedGroup = createSelector(
  selectCreateGroupFeature,
  (state: CreateGroupState) => state.createdGroup
);

export const selectCreateGroupError = createSelector(
  selectCreateGroupFeature,
  (state: CreateGroupState) => state.createGroupError
);

