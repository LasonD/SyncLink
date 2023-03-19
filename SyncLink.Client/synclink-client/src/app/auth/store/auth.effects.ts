// import { Actions, createEffect, ofType } from "@ngrx/effects";
// import {
//   AUTO_LOGIN_START, AutoLoginFailure,
//   LOGIN_START,
//   LOGIN_SUCCESS,
//   LoginFailure,
//   LoginStart,
//   LoginSuccess, LOGOUT,
//   SIGNUP_START,
//   SignupStart
// } from "./auth.actions";
// import { catchError, map, switchMap, tap, } from "rxjs/operators";
// import { HttpClient, HttpErrorResponse } from "@angular/common/http";
// import { Injectable } from "@angular/core";
// import { of } from "rxjs";
// import { Router } from "@angular/router";
// import { UserModel } from "../user.model";
//
// interface AuthResponse {
//   kind: string;
//   idToken: string;
//   refreshToken: string;
//   email: string;
//   displayName: string,
//   expiresIn: string;
//   localId: string;
// }
//
// @Injectable()
// export class AuthEffects {
//   private readonly userDataKey = 'userData';
//   readonly apiKey: string;
//
//   onLoginStart$ = createEffect(() => this.actions$.pipe(
//       ofType(LOGIN_START),
//       switchMap((authData: LoginStart) =>
//         this.httpClient.post<AuthResponse>(`https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=${this.apiKey}`,
//           {
//             email: authData.payload.email,
//             password: authData.payload.password,
//             returnSecureToken: true,
//           })),
//       map(this.handleAuthResponse.bind(this)),
//       catchError(this.handleError.bind(this)),
//     )
//   );
//
//   onLoginSuccess$ = createEffect(() =>
//     this.actions$.pipe(
//       ofType(LOGIN_SUCCESS),
//       tap((action: LoginSuccess) => {
//         if (action.payload.shouldRedirect) {
//           this.router.navigate(['/']);
//         }
//       })
//     ), {dispatch: false});
//
//   onSignupStart$ = createEffect(() => {
//     return this.actions$.pipe(
//       ofType(SIGNUP_START),
//       switchMap((signupData: SignupStart) =>
//         this.httpClient.post<AuthResponse>(`https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=${this.apiKey}`,
//           {
//             displayName: `${signupData.payload.firstName} ${signupData.payload.lastName}`.trim(),
//             email: signupData.payload.email,
//             password: signupData.payload.password,
//             returnSecureToken: true,
//           })),
//       map(this.handleAuthResponse.bind(this)),
//       catchError(this.handleError.bind(this)),
//     );
//   });
//
//   onAutoLogin$ = createEffect(() => this.actions$.pipe(
//     ofType(AUTO_LOGIN_START),
//     map(() => {
//       const userData: UserModel = JSON.parse(localStorage.getItem(this.userDataKey));
//
//       if (!userData) {
//         return new AutoLoginFailure();
//       }
//
//       const user = new User(
//         userData.id,
//         userData.userName,
//         userData.email,
//         userData._token,
//         null,
//         userData.expirationDate);
//
//       if (!user || !user.token) {
//         localStorage.removeItem(this.userDataKey);
//         return new AutoLoginFailure();
//       }
//
//       return new LoginSuccess({ user, shouldRedirect: false });
//     })
//   ));
//
//   onLogout$ = createEffect(() => this.actions$.pipe(
//     ofType(LOGOUT),
//     tap(() => {
//       localStorage.removeItem(this.userDataKey);
//       this.authService.clearAutoLogoutTimer();
//       this.router.navigate(['/auth']);
//     })
//   ), {dispatch: false});
//
//   constructor(private actions$: Actions,
//               private httpClient: HttpClient,
//               private router: Router,
//               private authService: AuthService,
//   ) {
//     this.apiKey = environment.apiKey;
//   }
//
//   private handleAuthResponse(authResponse: AuthResponse) {
//     const user = new User(
//       authResponse.localId,
//       authResponse.displayName,
//       authResponse.email,
//       authResponse.idToken,
//       +authResponse.expiresIn);
//
//     localStorage.setItem('userData', JSON.stringify(user));
//
//     this.authService.setAutoLogoutTimer(+authResponse.expiresIn * 1000);
//
//     return new LoginSuccess({ user, shouldRedirect: true });
//   }
//
//   private handleError(err: HttpErrorResponse) {
//     return of(new LoginFailure({authErrors: AuthEffects.extractResponseErrors(err)}));
//   }
//
//   private static extractResponseErrors(errorResponse: HttpErrorResponse): string[] {
//     const errorMessages = [];
//
//     for (const error of errorResponse?.error?.error?.errors?.map(e => e.message)) {
//       switch (error) {
//         case 'EMAIL_EXISTS':
//           errorMessages.push('This email is already registered.');
//           break;
//         case 'EMAIL_NOT_FOUND':
//         case 'INVALID_PASSWORD':
//           errorMessages.push('Invalid email or password.');
//           break;
//         case undefined:
//           continue;
//         default:
//           errorMessages.push(error);
//       }
//     }
//
//     return errorMessages;
//   }
// }
//
