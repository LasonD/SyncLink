import { createReducer, on } from '@ngrx/store';
import { Group } from '../models/group.model';
import { searchGroups, searchGroupsSuccess, searchGroupsFailure } from './groups.actions';

export interface State {
  groups: Group[];
  loading: boolean;
  error: any;
}

export const initialState: State = {
  groups: [],
  loading: false,
  error: null,
};

export const groupsReducer = createReducer(
  initialState,
  on(searchGroups, (state) => ({ ...state, loading: true })),
  on(searchGroupsSuccess, (state, { groups }) => ({ ...state, loading: false, groups })),
  on(searchGroupsFailure, (state, { error }) => ({ ...state, loading: false, error }))
);
