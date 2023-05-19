import { adapter, WhiteboardState } from "./whiteboard.reducer";
import { createFeatureSelector, createSelector } from "@ngrx/store";

export const selectWhiteboardFeature = createFeatureSelector<WhiteboardState>('whiteboards');

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

export const selectSelectedWhiteboardId = createSelector(
  selectWhiteboardFeature,
  (state: WhiteboardState) => state.selectedWhiteboardId
);

export const selectSelectedWhiteboard = createSelector(
  selectEntities,
  selectSelectedWhiteboardId,
  (entities, selectedId) => selectedId && entities[selectedId]
);
