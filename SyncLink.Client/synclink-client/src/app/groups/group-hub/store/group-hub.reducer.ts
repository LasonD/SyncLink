import { Group } from "../../models/group.model";
import { createReducer, on } from "@ngrx/store";
import { getGroup, getGroupFailure, getGroupSuccess } from "./group-hub.actions";

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

export const groupHubReducer = createReducer(
  initialState,
  on(getGroup, (state) : GroupHubState => ({ ...state, isLoading: true })),
  on(getGroupSuccess, (state, { group }) : GroupHubState => ({...state, group, isLoading: false })),
  on(getGroupFailure, (state, { error }) : GroupHubState => ({ ...state, error, isLoading: false })),
);
