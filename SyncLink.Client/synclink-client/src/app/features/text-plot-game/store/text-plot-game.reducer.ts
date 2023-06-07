import { combineReducers, createReducer, on } from '@ngrx/store';
import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";
import {
  entriesDiscardedExternal,
  entryCommittedExternal,
  entryVotedExternal,
  gameStartedExternal, getGamesSuccess, newEntryExternal,
  startGameSuccess,
  submitEntrySuccess, voteEntrySuccess,
} from "./text-plot-game.actions";

export const gamesAdapter: EntityAdapter<TextPlotGame> = createEntityAdapter<TextPlotGame>({
  sortComparer: (a, b) => {
    return -(new Date(a.creationDate)?.getTime() - new Date(b.creationDate)?.getTime());
  }
});
export const entryAdapter: EntityAdapter<TextPlotEntry> = createEntityAdapter<TextPlotEntry>({
  sortComparer: (a, b) => {
    return -(new Date(a.creationDate)?.getTime() - new Date(b.creationDate)?.getTime());
  }
});
export const voteAdapter: EntityAdapter<TextPlotVote> = createEntityAdapter<TextPlotVote>({
  sortComparer: (a, b) => {
    return -(new Date(a.creationDate)?.getTime() - new Date(b.creationDate)?.getTime());
  }
});

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
  on(getGamesSuccess, (state, action) => {
    return gamesAdapter.upsertMany(action.games?.entities, state)
  }),
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
  creationDate: Date;
}

export interface TextPlotGame {
  id: number;
  groupId: number;
  starterId: number;
  createdAt: Date;
  creationDate: Date;
  topic: string;
}

export interface TextPlotVote {
  id: number;
  userId: number;
  entryId: number;
  comment: string;
  creationDate: Date;
}
