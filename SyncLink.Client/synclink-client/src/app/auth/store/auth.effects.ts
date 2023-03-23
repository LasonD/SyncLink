import { Actions, createEffect, ofType } from "@ngrx/effects";
import {
  AUTO_LOGIN_START, AutoLoginFailure,
  LOGIN_START,
  LOGIN_SUCCESS,
  LoginFailure,
  LoginStart,
  LoginSuccess, LOGOUT,
  SIGNUP_START,
  SignupStart
} from "./auth.actions";
import { catchError, map, switchMap, tap, } from "rxjs/operators";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { of } from "rxjs";
import { Router } from "@angular/router";
import { AuthResult, User } from "../user.model";
import { environment } from "../../environments/environment";

@Injectable()
export class AuthEffects {
  private readonly userDataKey = 'userData';

  onLoginStart$ = createEffect(() => this.actions$.pipe(
      ofType(LOGIN_START),
      switchMap((authData: LoginStart) =>
        this.httpClient.post<AuthResult>(`${environment.apiBaseUrl}/api/auth/login`, authData.payload)),
      map(this.handleAuthResponse.bind(this)),
      catchError(this.handleError.bind(this)),
    )
  );

  onLoginSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LOGIN_SUCCESS),
      tap((action: LoginSuccess) => {
        if (action.payload.shouldRedirect) {
          this.router.navigate(['/']);
        }
      })
    ), {dispatch: false});

  onSignupStart$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SIGNUP_START),
      switchMap((signupData: SignupStart) => {
        return this.httpClient.post<AuthResult>(`${environment.apiBaseUrl}/api/auth/register`, signupData.payload);
      }),
      map(this.handleAuthResponse.bind(this)),
      catchError(this.handleError.bind(this)),
    );
  });

  onAutoLogin$ = createEffect(() => this.actions$.pipe(
    ofType(AUTO_LOGIN_START),
    map(() => {
      const authResult: AuthResult = JSON.parse(localStorage.getItem(this.userDataKey));

      if (!authResult) {
        return new AutoLoginFailure();
      }

      const user = new User(
        authResult.identityId,
        authResult.userId,
        authResult.username,
        authResult.firstName,
        authResult.lastName,
        authResult.email,
        authResult.accessToken,
        authResult.expiresIn);

      if (!user || !user.token) {
        localStorage.removeItem(this.userDataKey);
        return new AutoLoginFailure();
      }

      return new LoginSuccess({user, shouldRedirect: false});
    })
  ));

  onLogout$ = createEffect(() => this.actions$.pipe(
    ofType(LOGOUT),
    tap(() => {
      localStorage.removeItem(this.userDataKey);
      this.router.navigate(['/auth']);
    })
  ), {dispatch: false});

  constructor(private actions$: Actions,
              private httpClient: HttpClient,
              private router: Router
  ) {
  }

  private handleAuthResponse(authResponse: AuthResult) {
    const user = new User(
      authResponse.identityId,
      authResponse.userId,
      authResponse.username,
      authResponse.firstName,
      authResponse.lastName,
      authResponse.email,
      authResponse.accessToken,
      authResponse.expiresIn);

    localStorage.setItem('userData', JSON.stringify(user));

    return new LoginSuccess({user, shouldRedirect: true});
  }

  private handleError(err: HttpErrorResponse) {
    return of(new LoginFailure({authErrors: AuthEffects.extractResponseErrors(err)}));
  }

  private static extractResponseErrors(errorResponse: HttpErrorResponse): string[] {
    const errorMessages = [];

    console.log(errorResponse);

    // for (const error of errorResponse?.error?.error?.errors?.map(e => e.message)) {
    //   switch (error) {
    //     case 'EMAIL_EXISTS':
    //       errorMessages.push('This email is already registered.');
    //       break;
    //     case 'EMAIL_NOT_FOUND':
    //     case 'INVALID_PASSWORD':
    //       errorMessages.push('Invalid email or password.');
    //       break;
    //     case undefined:
    //       continue;
    //     default:
    //       errorMessages.push(error);
    //   }
    // }

    return errorMessages;
  }
}

