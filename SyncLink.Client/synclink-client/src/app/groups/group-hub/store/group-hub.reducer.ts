import { Group } from "../../models/group.model";
import { createReducer, on } from "@ngrx/store";
import { getGroupComplete, getGroupCompleteFailure, getGroupCompleteSuccess } from "./group-hub.actions";

export interface GroupHubState {
  group: Group;
  isLoading: boolean;
  error: any;
}

export const initialState: GroupHubState = {
  group: null,
  isLoading: false,
  error: null,
};

export const groupsHubReducer = createReducer(
  initialState,
  on(getGroupComplete, (state) : GroupHubState => ({ ...state, isLoading: true })),
  on(getGroupCompleteSuccess, (state, { group }) : GroupHubState => ({...state, group, isLoading: false })),
  on(getGroupCompleteFailure, (state, { error }) : GroupHubState => ({ ...state, error, isLoading: false })),
);
