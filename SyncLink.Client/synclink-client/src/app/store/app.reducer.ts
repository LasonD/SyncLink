import * as fromAuth from "../auth/store/auth.reducer";
import * as fromGroups from "../groups/store/groups.reducer";
import { ActionReducerMap } from "@ngrx/store";

export interface AppState {
  auth: fromAuth.State,
  groups: fromGroups.GroupsState,
}

export const appReducer: ActionReducerMap<AppState> = {
  auth: fromAuth.authReducer,
  groups: fromGroups.groupsReducer,
};
