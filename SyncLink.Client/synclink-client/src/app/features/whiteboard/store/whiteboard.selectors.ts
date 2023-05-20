import { Whiteboard, WhiteboardState } from "./whiteboard.reducer";
import { createFeatureSelector, createSelector } from "@ngrx/store";
import { FeaturesState } from "../../store/features.reducer";

export const selectFeaturesFeature = createFeatureSelector<FeaturesState>('features');

export const selectWhiteboardsState = createSelector(
  selectFeaturesFeature,
  (state: FeaturesState) => state.whiteboards
);

export const selectWhiteboards = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardState): Whiteboard[] => Object.values(state.entities)
);


export const selectSelectedWhiteboardId = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardState) => state.selectedWhiteboardId
);

export const selectCreatedWhiteboardId = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardState) => state.createdWhiteboardId
);

export const selectSelectedWhiteboard = createSelector(
  selectWhiteboardsState,
  selectSelectedWhiteboardId,
  (state, selectedId) => selectedId && state.entities[selectedId]
);
