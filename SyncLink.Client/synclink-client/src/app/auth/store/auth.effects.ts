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
import { catchError, map, mergeMap, tap, } from "rxjs/operators";
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
      mergeMap((authData: LoginStart) =>
        this.httpClient.post<AuthResult>(`${environment.apiBaseUrl}/api/auth/login`, authData.payload)
          .pipe(
            map(this.handleAuthResponse.bind(this)),
            catchError(this.handleError.bind(this))
          )
      )
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
      mergeMap((signupData: SignupStart) => {
        return this.httpClient.post<AuthResult>(`${environment.apiBaseUrl}/api/auth/register`, signupData.payload)
          .pipe(
            map(this.handleAuthResponse.bind(this)),
            catchError(this.handleError.bind(this))
          );
      }),
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
    const errors: Array<ModelStateError> | Array<string> = errorResponse?.error?.errors;
    let errorMessages: Array<string>;

    console.log(errors);

    if (errors?.length) {
      errorMessages = errors.flatMap((e: ModelStateError | string) => {
        if (typeof e === 'string') {
          return [e];
        }

        return [];
      });
    } else {
      errorMessages = ['Something went wrong...'];
    }

    return errorMessages;
  }
}

interface ModelStateError {
  [key: string]: string[];
}
