import { createAction, props } from "@ngrx/store";

export const createWordsChainGame = createAction(
  '[Words Chains] Create Words Chain Game',
  props<{ groupId: number, createWordsChainDto: { topic: string } }>()
);

export const sendWordEntry = createAction(
  '[Words Chains] Send Word Entry',
  props<{ groupId: number, gameId: number, createWordsChainDto: { topic: string } }>()
);

export const getWordsChainGames = createAction(
  '[Words Chains] Get Words Chain Games',
  props<{ groupId: number, pageNumber?: number, pageSize?: number }>()
);

export const getWordsChainGameById = createAction(
  '[Words Chains] Get Words Chain Game By Id',
  props<{ groupId: number, gameId: number }>()
);

export const getWordsChainGameEntries = createAction(
  '[Words Chains] Get Words Chain Game Entries',
  props<{ groupId: number, gameId: number, pageSize?: number, pageNumber?: number }>()
);

export const createWordsChainGameSuccess = createAction(
  '[Words Chains] Create Words Chain Game Success',
  props<{ response: any }>()
);

export const createWordsChainGameFailure = createAction(
  '[Words Chains] Create Words Chain Game Failure',
  props<{ error: any }>()
);

export const sendWordEntrySuccess = createAction(
  '[Words Chains] Send Word Entry Success',
  props<{ response: any }>()
);

export const sendWordEntryFailure = createAction(
  '[Words Chains] Send Word Entry Failure',
  props<{ error: any }>()
);

export const getWordsChainGamesSuccess = createAction(
  '[Words Chains] Get Words Chain Games Success',
  props<{ response: any }>()
);

export const getWordsChainGamesFailure = createAction(
  '[Words Chains] Get Words Chain Games Failure',
  props<{ error: any }>()
);

export const getWordsChainGameByIdSuccess = createAction(
  '[Words Chains] Get Words Chain Game By Id Success',
  props<{ response: any }>()
);

export const getWordsChainGameByIdFailure = createAction(
  '[Words Chains] Get Words Chain Game By Id Failure',
  props<{ error: any }>()
);

export const getWordsChainGameEntriesSuccess = createAction(
  '[Words Chains] Get Words Chain Game Entries Success',
  props<{ response: any }>()
);

export const getWordsChainGameEntriesFailure = createAction(
  '[Words Chains] Get Words Chain Game Entries Failure',
  props<{ error: any }>()
);

