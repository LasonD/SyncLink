import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';

import * as WordsChainsActions from './words-chain.actions';

@Injectable()
export class WordsChainsEffects {

  createWordsChainGame$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WordsChainsActions.createWordsChainGame),
      mergeMap(({ groupId, createWordsChainDto }) =>
        this.http.post(`/api/groups/${groupId}/features/wordschains`, createWordsChainDto).pipe(
          map(response => WordsChainsActions.createWordsChainGameSuccess({ response })),
          catchError(error => of(WordsChainsActions.createWordsChainGameFailure({ error })))
        )
      )
    )
  );

  sendWordEntry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WordsChainsActions.sendWordEntry),
      mergeMap(({ groupId, gameId, createWordsChainDto }) =>
        this.http.post(`/api/groups/${groupId}/features/wordschains/${gameId}`, createWordsChainDto).pipe(
          map(response => WordsChainsActions.sendWordEntrySuccess({ response })),
          catchError(error => of(WordsChainsActions.sendWordEntryFailure({ error })))
        )
      )
    )
  );

  getWordsChainGames$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WordsChainsActions.getWordsChainGames),
      mergeMap(({ groupId, pageNumber, pageSize }) =>
        this.http.get(`/api/groups/${groupId}/features/wordschains`, { params: { pageNumber, pageSize } }).pipe(
          map(response => WordsChainsActions.getWordsChainGamesSuccess({ response })),
          catchError(error => of(WordsChainsActions.getWordsChainGamesFailure({ error })))
        )
      )
    )
  );

  getWordsChainGameById$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WordsChainsActions.getWordsChainGameById),
      mergeMap(({ groupId, gameId }) =>
        this.http.get(`/api/groups/${groupId}/features/wordschains/${gameId}`).pipe(
          map(response => WordsChainsActions.getWordsChainGameByIdSuccess({ response })),
          catchError(error => of(WordsChainsActions.getWordsChainGameByIdFailure({ error })))
        )
      )
    )
  );

  getWordsChainGameEntries$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WordsChainsActions.getWordsChainGameEntries),
      mergeMap(({ groupId, gameId, pageSize, pageNumber }) =>
        this.http.get(`/api/groups/${groupId}/features/wordschains/${gameId}/entries`, { params: { pageSize, pageNumber } }).pipe(
          map(response => WordsChainsActions.getWordsChainGameEntriesSuccess({ response })),
          catchError(error => of(WordsChainsActions.getWordsChainGameEntriesFailure({ error })))
        )
      )
    )
  );

  constructor(
    private actions$: Actions,
    private http: HttpClient
  ) { }

}
