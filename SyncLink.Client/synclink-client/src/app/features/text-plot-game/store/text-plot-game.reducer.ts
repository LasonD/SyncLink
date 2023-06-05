import { createReducer, on } from '@ngrx/store';
import * as TextPlotGameActions from './text-plot-game.actions';
import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";

export const adapter: EntityAdapter<TextPlotGame> = createEntityAdapter<TextPlotGame>();
export const entryAdapter: EntityAdapter<TextPlotEntry> = createEntityAdapter<TextPlotEntry>();
export const voteAdapter: EntityAdapter<TextPlotVote> = createEntityAdapter<TextPlotVote>();

export interface TextPlotGameState {
  games: EntityState<TextPlotGame>;
  entries: EntityState<TextPlotEntry>;
  votes: EntityState<TextPlotVote>;
}

export const initialState: TextPlotGameState = {
  games: adapter.getInitialState(),
  entries: entryAdapter.getInitialState(),
  votes: voteAdapter.getInitialState(),
};

export const textPlotGameReducer = createReducer(
  initialState,
  on(TextPlotGameActions.startGameSuccess, (state, action) => {
    return {
      ...state,
      game: adapter.setOne(action.game, state.games),
      entries: entryAdapter.removeAll(state.entries),
      votes: voteAdapter.removeAll(state.votes),
    };
  }),
  on(TextPlotGameActions.submitEntrySuccess, (state, action) => {
    return {
      ...state,
      entries: entryAdapter.addOne(action.entry, state.entries),
    };
  }),
  on(TextPlotGameActions.voteEntrySuccess, (state, action) => {
    return {
      ...state,
      votes: voteAdapter.addOne(action.vote, state.votes),
    };
  }),
  on(TextPlotGameActions.endGameSuccess, (state, action) => {
    return {
      ...state,
      game: adapter.removeAll(state.games),
    };
  }),
  on(TextPlotGameActions.gameStartedExternal, (state, action) => {
    return {
      ...state,
    };
  })
);

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
}

export interface TextPlotVote {
  id: number;
  userId: number;
  entryId: number;
  comment: string;
}
