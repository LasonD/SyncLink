import { createReducer, on } from '@ngrx/store';
import { Group } from "../../../models/group.model";
import { createGroup, createGroupError, createGroupSuccess } from "./create-group.actions";

export interface CreateGroupState {
  createGroupLoading: boolean;
  createGroupError: any;
  createdGroup: Group;
}

export const initialState: CreateGroupState = {
  createGroupLoading: false,
  createGroupError: null,
  createdGroup: null,
};

export const createGroupReducer = createReducer(
  initialState,
  on(createGroup, (state) => ({ ...state, createGroupLoading: true })),
  on(createGroupSuccess, (state, { group }) => ({ ...state, createdGroup: group, createGroupLoading: false })),
  on(createGroupError, (state, { error }) => ({ ...state, createdGroup: null, createGroupLoading: false, error: error })),
);
