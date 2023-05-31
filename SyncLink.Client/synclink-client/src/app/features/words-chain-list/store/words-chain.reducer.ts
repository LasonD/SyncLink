import { Page } from "../../../models/pagination.model";
import * as WordsChainsActions from './words-chain.actions';
import { createEntityAdapter, EntityState } from '@ngrx/entity';
import { createReducer, on } from "@ngrx/store";

export interface WordsChainState {
  wordsChainDetailsState: WordsChainDetailsState,
  wordsChainOverviewState: WordsChainOverviewState,
}

export interface WordsChainOverviewState extends EntityState<WordsChainOverview> { }

export const wordsChainOverviewAdapter = createEntityAdapter<WordsChainOverview>({
  selectId: (overview: WordsChainOverview) => overview.id
});

export interface WordsChainDetailsState extends EntityState<WordsChain> {
  selectedWordsChainId: number | null;
}

export const wordsChainAdapter = createEntityAdapter<WordsChain>({
  selectId: (chain: WordsChain) => chain.id
});

export const initialWordsChainOverviewState: WordsChainOverviewState = wordsChainOverviewAdapter.getInitialState();

export const initialWordsChainState: WordsChainDetailsState = wordsChainAdapter.getInitialState({
  selectedWordsChainId: null
});

export const wordsChainsOverviewReducer = createReducer(
  initialWordsChainOverviewState,

  on(WordsChainsActions.getWordsChainGamesSuccess, (state, { response }) => {
    return wordsChainOverviewAdapter.setAll(response, state);
  })
);

export const wordsChainsReducer = createReducer(
  initialWordsChainState,

  on(WordsChainsActions.getWordsChainGameByIdSuccess, (state, { response }) => {
    return wordsChainAdapter.upsertOne(response, { ...state, selectedWordsChainId: response.id });
  }),

  on(WordsChainsActions.createWordsChainGameSuccess, (state, { response }) => {
    return wordsChainAdapter.addOne(response, state);
  }),

  on(WordsChainsActions.sendWordEntrySuccess, (state, { response }) => {
    if (state.selectedWordsChainId !== null && state.selectedWordsChainId === response.id) {
      return wordsChainAdapter.updateOne({ id: response.id, changes: response }, state);
    }
    return state;
  })
);

export interface WordsChainOverview {
  id: number,
}

export interface WordsChain {
  id: number,
  topic: string,
  words: Page<WordChainEntry>,
  participants: WordsChainParticipant[],
}

export interface WordChainEntry {
  word: string,
  senderId: number,
}

export interface WordsChainParticipant {
  id: number,
  username: string,
  score: number,
}
