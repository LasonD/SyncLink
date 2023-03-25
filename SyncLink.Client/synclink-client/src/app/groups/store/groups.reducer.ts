import { createReducer, on } from '@ngrx/store';
import { Group } from '../models/group.model';
import {
  searchGroups,
  searchGroupsSuccess,
  searchGroupsFailure,
  createGroup,
  createGroupSuccess, createGroupError
} from './groups.actions';

export interface State {
  groups: Group[];
  searchGroupLoading: boolean;
  createGroupLoading: boolean;
  error: any;
  createdGroup: Group;
}

export const initialState: State = {
  groups: [],
  searchGroupLoading: false,
  createGroupLoading: false,
  error: null,
  createdGroup: null,
};

export const groupsReducer = createReducer(
  initialState,
  on(searchGroups, (state) => ({ ...state, loading: true })),
  on(searchGroupsSuccess, (state, { groups }) => ({ ...state, searchGroupLoading: false, groups })),
  on(searchGroupsFailure, (state, { error }) => ({ ...state, searchGroupLoading: false, error })),

  on(createGroup, (state) => ({ ...state, createGroupLoading: true })),
  on(createGroupSuccess, (state, { group }) => ({ ...state, createdGroup: group, createGroupLoading: false })),
  on(createGroupError, (state, { error }) => ({ ...state, createdGroup: null, createGroupLoading: false, error: error })),
);
