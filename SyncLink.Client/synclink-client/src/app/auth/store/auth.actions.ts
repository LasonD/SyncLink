import { Action } from "@ngrx/store";
import { User } from "../user.model";

export const LOGIN_START = "[Auth] LOGIN_START";
export const LOGIN_SUCCESS = "[Auth] LOGIN";
export const LOGIN_FAILURE = "[Auth] LOGIN_FAILURE";
export const SIGNUP_START = "[Auth] SIGNUP_START"
export const CLEAR_ERRORS = "[Auth] CLEAR_ERRORS"
export const AUTO_LOGIN_START = "[Auth] AUTO_LOGIN_START"
export const AUTO_LOGIN_FAILURE = "[Auth] AUTO_LOGIN_FAILURE"
export const LOGOUT = "[Auth] LOGOUT";

export class LoginStart implements Action {
  readonly type = LOGIN_START;

  constructor(public payload: { email: string, password: number }) {
  }
}

export class LoginSuccess implements Action {
  readonly type = LOGIN_SUCCESS;

  constructor(public payload: { user: User, shouldRedirect: boolean }) {
  }
}

export class LoginFailure implements Action {
  readonly type = LOGIN_FAILURE;

  constructor(public payload: { authErrors: string[] }) {
  }
}

export class SignupStart implements Action {
  readonly type = SIGNUP_START;

  constructor(public payload: {firstName: string, lastName: string, email: string, password: string}) {
  }
}

export class Logout implements Action {
  readonly type = LOGOUT;
}

export class ClearErrors implements Action {
  readonly type = CLEAR_ERRORS;
}

export class AutoLoginStart implements Action {
  readonly type = AUTO_LOGIN_START;
}

export class AutoLoginFailure implements Action {
  readonly type = AUTO_LOGIN_FAILURE;
}

export type AuthActions = LoginSuccess
  | Logout
  | LoginFailure
  | LoginStart
  | SignupStart
  | ClearErrors
  | AutoLoginStart
  | AutoLoginFailure;
