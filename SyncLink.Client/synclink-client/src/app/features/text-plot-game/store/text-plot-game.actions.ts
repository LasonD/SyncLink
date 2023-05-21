import { createAction, props } from "@ngrx/store";
import { TextPlotEntry, TextPlotGame, TextPlotVote } from "./text-plot-game.reducer";

export const gameStarted = createAction('[Text Plot Game] Game Started', props<{ game: TextPlotGame }>());
export const newEntrySubmitted = createAction('[Text Plot Game] New Entry Submitted', props<{ entry: TextPlotEntry }>());
export const voteReceived = createAction('[Text Plot Game] Vote Received', props<{ vote: TextPlotVote }>());
export const gameEnded = createAction('[Text Plot Game] Game Ended', props<{ game: TextPlotGame }>());
