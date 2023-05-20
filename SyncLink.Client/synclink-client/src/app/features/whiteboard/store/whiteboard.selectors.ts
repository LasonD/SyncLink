import { adapter, WhiteboardState } from "./whiteboard.reducer";
import { createFeatureSelector, createSelector } from "@ngrx/store";
import { FeaturesState } from "../../store/features.reducer";

export const selectFeaturesFeature = createFeatureSelector<FeaturesState>('features');

export const selectWhiteboardsState = createSelector(
  selectFeaturesFeature,
  (state: FeaturesState) => state.whiteboards
);

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

export const selectSelectedWhiteboardId = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardState) => state.selectedWhiteboardId
);

export const selectCreatedWhiteboardId = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardState) => state.createdWhiteboardId
);

export const selectSelectedWhiteboard = createSelector(
  selectEntities,
  selectSelectedWhiteboardId,
  (entities, selectedId) => selectedId && entities[selectedId]
);
