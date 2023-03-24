import * as fromAuth from "../auth/store/auth.reducer";
import * as fromGroupsSearch from "../groups-search/store/groups-search.reducer";
import { ActionReducerMap } from "@ngrx/store";

export interface AppState {
  auth: fromAuth.State,
  groupsSearch: fromGroupsSearch.State,
}

export const appReducer: ActionReducerMap<AppState> = {
  auth: fromAuth.authReducer,
  groupsSearch: fromGroupsSearch.groupSearchReducer,
};
