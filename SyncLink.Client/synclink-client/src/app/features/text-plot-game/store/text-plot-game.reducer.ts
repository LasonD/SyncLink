import { combineReducers, createReducer, on } from '@ngrx/store';
import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";
import {
  entriesDiscardedExternal,
  entryCommittedExternal,
  entryVotedExternal,
  gameStartedExternal, getGameWithEntries, getGameWithEntriesSuccess, getGamesSuccess, newEntryExternal,
  startGameSuccess,
  submitEntrySuccess, voteEntrySuccess, voteRevokedExternal, votingTimerProgressExternal,
} from "./text-plot-game.actions";

export const gamesAdapter: EntityAdapter<TextPlotGame> = createEntityAdapter<TextPlotGame>({
  sortComparer: (a, b) => {
    return -(new Date(a.creationDate)?.getTime() - new Date(b.creationDate)?.getTime());
  }
});
export const entryAdapter: EntityAdapter<TextPlotEntry> = createEntityAdapter<TextPlotEntry>({
  sortComparer: (a, b) => {
    return (new Date(a.creationDate)?.getTime() - new Date(b.creationDate)?.getTime());
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
  selectedGameId: number,
  selectedGameVotingTimerProgress: number,
}

export interface TextPlotEntriesState extends EntityState<TextPlotEntry> {

}

export interface TextPlotVotesState extends EntityState<TextPlotVote> {

}

export const textPlotGamesInitialState: TextPlotGamesState = gamesAdapter.getInitialState({
  createdGame: null,
  selectedGameId: null,
  selectedGameVotingTimerProgress: null,
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
  on(getGameWithEntries, (state, action) => {
    return {...state, selectedGameId: action.gameId};
  }),
  on(getGameWithEntriesSuccess, (state, action) => {
    return gamesAdapter.upsertOne(action.game, state);
  }),
  on(votingTimerProgressExternal, (state, action) => {
    if (action.gameId !== state.selectedGameId) {
      return state;
    }

    return {...state, selectedGameVotingTimerProgress: action.percent};
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
  }),
  on(getGameWithEntriesSuccess, (state, action) => {
    return entryAdapter.upsertMany(action.game?.entries, state);
  })
);

export const textPlotVotesReducer = createReducer(
  textPlotVotesInitialState,
  on(voteEntrySuccess, (state, action) => {
    return voteAdapter.upsertOne(action.vote, state);
  }),
  on(entryVotedExternal, (state, action) => {
    return voteAdapter.upsertOne(action.vote, state);
  }),
  on(voteRevokedExternal, (state, action) => {
    return voteAdapter.removeOne(action.voteId, state);
  }),
  on(getGameWithEntriesSuccess, (state, action) => {
    return voteAdapter.upsertMany(action.game?.entries?.flatMap(e => e.votes), state);
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
  isCommitted: boolean;
  votes: TextPlotVote[];
}

export interface TextPlotGame {
  id: number;
  groupId: number;
  starterId: number;
  createdAt: Date;
  creationDate: Date;
  topic: string;
}

export interface TextPlotGameWithEntries extends TextPlotGame {
  entries: TextPlotEntry[]
}

export interface TextPlotVote {
  id: number;
  userId: number;
  entryId: number;
  comment: string;
  score: number;
  creationDate: Date;
}

export interface TextPlotGameStats {
  gameId: number;
  groupId: number;
  topic: string;
  entriesCommittedCount: number;
  wordsCommittedCount: number;
  userStats: TextPlotGameUserStats[];
}

export interface TextPlotGameUserStats {
  userId: number;
  username: string;
  entriesCommittedCount: number;
  entriesSubmittedCount: number;
  wordsCommittedCount: number;
  wordsSubmittedCount: number;
  totalReceivedScore: number;
  votesLeftCount: number;
  commentsReceived: string[];
}

