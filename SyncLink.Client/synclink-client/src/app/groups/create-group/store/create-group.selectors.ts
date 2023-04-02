import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CreateGroupState } from "./create-group.reducer";

export const selectCreateGroupFeature = createFeatureSelector<CreateGroupState>('createGroup');

export const selectCreatedGroup = createSelector(
  selectCreateGroupFeature,
  (state: CreateGroupState) => state.createdGroup
);

export const selectCreateGroupError = createSelector(
  selectCreateGroupFeature,
  (state: CreateGroupState) => state.createGroupError
);

