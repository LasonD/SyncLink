import {
  AuthActions,
  CLEAR_ERRORS,
  LOGIN_FAILURE,
  LOGIN_START,
  LOGIN_SUCCESS,
  LOGOUT,
  SIGNUP_START
} from "./auth.actions";
import { User } from "../user.model";

export interface State {
  user: User
  isLoading: boolean,
  authErrorMessages?: string[],
}

const initialState: State = {
  user: null,
  isLoading: false,
  authErrorMessages: null,
}

export function authReducer(state = initialState, action: AuthActions): State {
  switch (action.type) {
    case LOGIN_SUCCESS:
      return {
        ...state,
        user: action.payload.user,
        isLoading: false,
        authErrorMessages: null,
      };
    case LOGIN_START:
    case SIGNUP_START:
      return {
        ...state,
        authErrorMessages: null,
        isLoading: true,
      };
    case LOGIN_FAILURE:
      return {
        ...state,
        user: null,
        authErrorMessages: action.payload.authErrors,
        isLoading: false
      };
    case LOGOUT:
      return {
        ...state,
        user: null,
        isLoading: false
      };
    case CLEAR_ERRORS:
      return {...state, authErrorMessages: null};
    default:
      return state;
  }
}
