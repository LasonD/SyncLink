import { createSelector } from "@ngrx/store";
import { selectFeaturesFeature } from "../../whiteboard/store/whiteboard.selectors";
import { entryAdapter, gamesAdapter, voteAdapter } from "./text-plot-game.reducer";
import { AppState } from "../../../store/app.reducer";

export const gameSelectors = gamesAdapter.getSelectors((state: AppState) => state.features.textPlotGame.games);
export const entriesSelectors = entryAdapter.getSelectors((state: AppState) => state.features.textPlotGame.entries);
export const votesSelectors = voteAdapter.getSelectors((state: AppState) => state.features.textPlotGame.votes);

export const selectTextPlotGameState = createSelector(
  selectFeaturesFeature,
  (state) => state.textPlotGame
);

export const selectGame = createSelector(
  selectTextPlotGameState,
  state => state.games
);

export const selectEntries = createSelector(
  selectTextPlotGameState,
  state => state.entries
);

export const selectVotes = createSelector(
  selectTextPlotGameState,
  state => state.votes
);

export const selectEntriesByGameId = (gameId: number) => createSelector(
  entriesSelectors.selectAll,
  entries => entries.filter(e => e.gameId === gameId)
);

export const selectGamesByGroupId = (groupId: number) => createSelector(
  gameSelectors.selectAll,
  entries => entries.filter(e => e.groupId === groupId)
);
