import { createSelector } from "@ngrx/store";
import { selectFeaturesFeature } from "../../whiteboard/store/whiteboard.selectors";

export const selectTextPlotGameState = createSelector(
  selectFeaturesFeature,
  (state) => state.textPlotGame
);

export const selectGame = createSelector(
  selectTextPlotGameState,
  state => state.game
);

export const selectEntries = createSelector(
  selectTextPlotGameState,
  state => state.entries
);

export const selectVotes = createSelector(
  selectTextPlotGameState,
  state => state.votes
);
