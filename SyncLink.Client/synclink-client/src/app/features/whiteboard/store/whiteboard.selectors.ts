import { Whiteboard, whiteboardElementsAdapter, whiteboardsAdapter, WhiteboardsState } from "./whiteboard.reducer";
import { createFeatureSelector, createSelector } from "@ngrx/store";
import { FeaturesState } from "../../store/features.reducer";

export const selectFeaturesFeature = createFeatureSelector<FeaturesState>('features');

export const whiteboardsSelectors = whiteboardsAdapter.getSelectors();
export const whiteboardElementsSelectors = whiteboardElementsAdapter.getSelectors();

export const selectWhiteboardsState = createSelector(
  selectFeaturesFeature,
  (state: FeaturesState) => state.whiteboards.whiteboards
);

export const selectWhiteboardLoading = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardsState): boolean => state.whiteboardLoading
);

export const selectWhiteboards = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardsState): Whiteboard[] => Object.values(state.entities)
);

export const selectSelectedWhiteboardId = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardsState) => state.selectedWhiteboardId
);

export const selectCreatedWhiteboardId = createSelector(
  selectWhiteboardsState,
  (state: WhiteboardsState) => state.createdWhiteboardId
);

export const selectSelectedWhiteboard = createSelector(
  selectWhiteboardsState,
  selectSelectedWhiteboardId,
  (state, selectedId) => selectedId && state.entities[selectedId]
);

export const selectWhiteboardElementsById = (whiteboardId: number) => createSelector(
  whiteboardElementsSelectors.selectAll,
  (elements) => elements.filter(e => e.whiteboardId === whiteboardId)
);
