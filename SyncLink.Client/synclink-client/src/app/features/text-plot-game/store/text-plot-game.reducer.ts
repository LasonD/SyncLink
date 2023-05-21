import { createReducer, on } from '@ngrx/store';
import * as TextPlotGameActions from './text-plot-game.actions';
import { User } from "../../../auth/user.model";
import { Group } from "../../../models/group.model";
import { EntityState } from "@ngrx/entity";

export interface TextPlotGameState {
  game: TextPlotGame | null;
  entries: TextPlotEntry[];
  votes: TextPlotVote[];
}

export const initialState: TextPlotGameState = {
  game: null,
  entries: [],
  votes: []
};

export const textPlotGameReducer = createReducer(
  initialState,

  on(TextPlotGameActions.gameStarted, (state, action) => ({ ...state, game: action.game, entries: [], votes: [] })),
  on(TextPlotGameActions.newEntrySubmitted, (state, action) => ({ ...state, entries: [...state.entries, action.entry] })),
  on(TextPlotGameActions.voteReceived, (state, action) => ({ ...state, votes: [...state.votes, action.vote] })),
  on(TextPlotGameActions.gameEnded, (state, action) => ({ ...state, game: null }))
);

export interface TextPlotGame {
  id: number;
  group: Group;
  startedBy: User;
  startedAt: Date;
  endedAt: Date | null;
}

export interface TextPlotEntry {
  id: number;
  game: TextPlotGame;
  submittedBy: User;
  text: string;
  submittedAt: Date;
}

export interface TextPlotVote {
  id: number;
  entry: TextPlotEntry;
  votedBy: User;
  votedAt: Date;
}
