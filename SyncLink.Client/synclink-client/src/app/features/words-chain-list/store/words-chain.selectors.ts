import { createSelector } from '@ngrx/store';
import { wordsChainOverviewAdapter, wordsChainAdapter } from './words-chain.reducer';
import { selectFeaturesFeature } from "../../whiteboard/store/whiteboard.selectors";
import { FeaturesState } from "../../store/features.reducer";

export const selectWordsChainOverviewFeature = createSelector(
  selectFeaturesFeature,
  (state: FeaturesState) => state.wordsChainState.wordsChainOverviewState
);
export const selectWordsChainDetailsFeature = createSelector(
  selectFeaturesFeature,
  (state: FeaturesState) => state.wordsChainState.wordsChainDetailsState);

export const {
  selectIds: selectWordsChainOverviewIds,
  selectEntities: selectWordsChainOverviewEntities,
  selectAll: selectAllWordsChainOverviews,
  selectTotal: selectTotalWordsChainOverviews,
} = wordsChainOverviewAdapter.getSelectors(selectWordsChainOverviewFeature);

export const {
  selectIds: selectWordsChainEntriesIds,
  selectEntities: selectWordsChainEntryEntities,
  selectAll: selectAllWordsChainEntries,
  selectTotal: selectTotalWordsChainsEntries,
} = wordsChainAdapter.getSelectors(selectWordsChainDetailsFeature);

export const selectCurrentWordsChain = createSelector(
  selectWordsChainEntryEntities,
  selectWordsChainDetailsFeature,
  (entities, wordsChainState) => entities[wordsChainState.selectedWordsChainId]
);
