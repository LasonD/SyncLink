import { createAction, props } from "@ngrx/store";
import { TextPlotEntry, TextPlotGame, TextPlotVote } from "./text-plot-game.reducer";

export const startGame = createAction('[Text Plot Game] Start Game', props<{ game: TextPlotGame, groupId: number }>());
export const startGameSuccess = createAction('[Text Plot Game] Start Game Success', props<{ game: TextPlotGame }>());
export const startGameFailure = createAction('[Text Plot Game] Start Game Failure', props<{ error: any }>());

export const submitEntry = createAction('[Text Plot Game] Submit Entry', props<{ entry: TextPlotEntry }>());
export const submitEntrySuccess = createAction('[Text Plot Game] Submit Entry Success', props<{ entry: TextPlotEntry }>());
export const submitEntryFailure = createAction('[Text Plot Game] Submit Entry Failure', props<{ error: any }>());

export const voteEntry = createAction('[Text Plot Game] Vote Entry', props<{ vote: TextPlotVote }>());
export const voteEntrySuccess = createAction('[Text Plot Game] Vote Entry Success', props<{ vote: TextPlotVote }>());
export const voteEntryFailure = createAction('[Text Plot Game] Vote Entry Failure', props<{ error: any }>());

export const endGame = createAction('[Text Plot Game] End Game', props<{ gameId: number }>());
export const endGameSuccess = createAction('[Text Plot Game] End Game Success');
export const endGameFailure = createAction('[Text Plot Game] End Game Failure', props<{ error: any }>());

export const gameStartedExternal = createAction('[Text Plot Game] Game Started External', props<{ game: TextPlotGame }>());
export const gameEndedExternal = createAction('[Text Plot Game] Game Ended External', props<{ game: TextPlotGame }>());
export const newEntryExternal = createAction('[Text Plot Game] New Entry External', props<{ entry: TextPlotEntry }>());
export const entryVotedExternal = createAction('[Text Plot Game] Entry Voted External', props<{ vote: TextPlotVote }>());
