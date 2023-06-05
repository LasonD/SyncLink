import { combineReducers, createReducer, on } from '@ngrx/store';
import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";
import {
  entriesDiscardedExternal,
  entryCommittedExternal,
  entryVotedExternal,
  gameStartedExternal, newEntryExternal,
  startGameSuccess,
  submitEntrySuccess, voteEntrySuccess,
} from "./text-plot-game.actions";

export const gamesAdapter: EntityAdapter<TextPlotGame> = createEntityAdapter<TextPlotGame>();
export const entryAdapter: EntityAdapter<TextPlotEntry> = createEntityAdapter<TextPlotEntry>();
export const voteAdapter: EntityAdapter<TextPlotVote> = createEntityAdapter<TextPlotVote>();

export interface TextPlotGameState {
  games: TextPlotGamesState;
  entries: TextPlotEntriesState;
  votes: TextPlotVotesState;
}

export interface TextPlotGamesState extends EntityState<TextPlotGame> {
  createdGame: TextPlotGame,
}

export interface TextPlotEntriesState extends EntityState<TextPlotEntry> {

}

export interface TextPlotVotesState extends EntityState<TextPlotVote> {

}

export const textPlotGamesInitialState: TextPlotGamesState = gamesAdapter.getInitialState({
  createdGame: null
});
export const textPlotEntriesInitialState = entryAdapter.getInitialState();
export const textPlotVotesInitialState = voteAdapter.getInitialState();

export const textPlotGamesReducer = createReducer(
  textPlotGamesInitialState,
  on(startGameSuccess, (state, action) => {
    return gamesAdapter.upsertOne(action.game, {...state, createdGame: action.game });
  }),
  on(gameStartedExternal, (state, action) => {
    return gamesAdapter.upsertOne(action.game, state);
  }),
);

export const textPlotEntriesReducer = createReducer(
  textPlotEntriesInitialState,
  on(submitEntrySuccess, (state, action) => {
    return entryAdapter.upsertOne(action.entry, state);
  }),
  on(newEntryExternal, (state, action) => {
    return entryAdapter.upsertOne(action.entry, state);
  }),
  on(entryCommittedExternal, (state, action) => {
    return entryAdapter.upsertOne(action.entry, state);
  }),
  on(entriesDiscardedExternal, (state, action) => {
    return entryAdapter.removeMany(action.discardedEntryIds, state)
  })
);

export const textPlotVotesReducer = createReducer(
  textPlotVotesInitialState,
  on(voteEntrySuccess, (state, action) => {
    return voteAdapter.upsertOne(action.vote, state);
  }),
  on(entryVotedExternal, (state, action) => {
    return voteAdapter.upsertOne(action.vote, state);
  })
);

export const textPlotGamesFeatureReducer = combineReducers({
  games: textPlotGamesReducer,
  entries: textPlotEntriesReducer,
  votes: textPlotVotesReducer
});

export interface TextPlotEntry {
  id: number;
  userId: number;
  gameId: number;
  text: string;
  createdAt: Date;
}

export interface TextPlotGame {
  id: number;
  groupId: number;
  starterId: number;
  startedAt: Date;
  endedAt: Date | null;
  topic: string;
}

export interface TextPlotVote {
  id: number;
  userId: number;
  entryId: number;
  comment: string;
}
