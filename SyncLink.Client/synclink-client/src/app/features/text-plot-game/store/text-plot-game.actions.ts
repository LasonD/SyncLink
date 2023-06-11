import { createAction, props } from "@ngrx/store";
import { TextPlotEntry, TextPlotGame, TextPlotGameWithEntries, TextPlotVote } from "./text-plot-game.reducer";
import { StartTextPlotGameData, SubmitTextPlotEntryData, SubmitTextPlotVoteData } from "./text-plot-game.effects";
import { Page } from "../../../models/pagination.model";

export const getGames = createAction('[Text Plot Game] Get Games', props<{ groupId: number }>())
export const getGamesSuccess = createAction('[Text Plot Game] Get Games Success', props<{ games: Page<TextPlotGame> }>());
export const getGamesFailure = createAction('[Text Plot Game] Get Games Failure', props<{ error: any }>());
export const startGame = createAction('[Text Plot Game] Start Game', props<{ game: StartTextPlotGameData, groupId: number }>());
export const startGameSuccess = createAction('[Text Plot Game] Start Game Success', props<{ game: TextPlotGame }>());
export const startGameFailure = createAction('[Text Plot Game] Start Game Failure', props<{ error: any }>());

export const getGameWithEntries = createAction('[Text Plot Game] Get Game With Entries', props<{ groupId: number, gameId: number }>());
export const getGameWithEntriesSuccess = createAction('[Text Plot Game] Get Game With Entries Success', props<{ game: TextPlotGameWithEntries }>());
export const getGameWithEntriesFailure = createAction('[Text Plot Game] Get Game With Entries Failure', props<{ error: any }>());
export const submitEntry = createAction('[Text Plot Game] Submit Entry', props<{ groupId: number, gameId: number, entry: SubmitTextPlotEntryData }>());
export const submitEntrySuccess = createAction('[Text Plot Game] Submit Entry Success', props<{ entry: TextPlotEntry }>());
export const submitEntryFailure = createAction('[Text Plot Game] Submit Entry Failure', props<{ error: any }>());

export const voteEntry = createAction('[Text Plot Game] Vote Entry', props<{ gameId: number, groupId: number, entryId: number, vote: SubmitTextPlotVoteData }>());
export const voteEntrySuccess = createAction('[Text Plot Game] Vote Entry Success', props<{ vote: TextPlotVote }>());
export const voteEntryFailure = createAction('[Text Plot Game] Vote Entry Failure', props<{ error: any }>());

export const revokeVote = createAction('[Text Plot Game] Revoke Vote', props<{ gameId: number, groupId: number, entryId: number }>());
export const revokeVoteSuccess = createAction('[Text Plot Game] Revoke Vote Success');
export const revokeVoteFailure = createAction('[Text Plot Game] Revoke Vote Failure', props<{ error: any }>());

export const endGame = createAction('[Text Plot Game] End Game', props<{ gameId: number }>());
export const endGameSuccess = createAction('[Text Plot Game] End Game Success');
export const endGameFailure = createAction('[Text Plot Game] End Game Failure', props<{ error: any }>());

export const enterGame = createAction('[Text Plot Game] Enter Game', props<{ groupId: number, gameId: number }>());

export const gameStartedExternal = createAction('[Text Plot Game] Game Started External', props<{ game: TextPlotGame }>());
export const gameEndedExternal = createAction('[Text Plot Game] Game Ended External', props<{ game: TextPlotGame }>());
export const newEntryExternal = createAction('[Text Plot Game] New Entry External', props<{ entry: TextPlotEntry }>());
export const entriesDiscardedExternal = createAction('[Text Plot Game] Entries Discarded External', props<{ discardedEntryIds: number[] }>());
export const entryCommittedExternal = createAction('[Text Plot Game] Entry Committed External', props<{ entry: TextPlotEntry }>());
export const entryVotedExternal = createAction('[Text Plot Game] Entry Voted External', props<{ vote: TextPlotVote }>());
export const voteRevokedExternal = createAction('[Text Plot Game] Vote Revoked External', props<{ voteId: number }>());
