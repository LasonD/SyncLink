import { groupsJoinRequestsReducer, JoinRequestsState } from "../join-requests/store/join-requests.reducer";
import { combineReducers } from "@ngrx/store";

export interface AdminState {
  joinRequests: JoinRequestsState
}

export const adminReducer = combineReducers<AdminState>({
  joinRequests: groupsJoinRequestsReducer
});
