import { createFeatureSelector, createSelector } from '@ngrx/store';
import { State } from './groups.reducer';

export const selectGroupSearchFeature = createFeatureSelector<State>('groups');

export const selectGroups = createSelector(
  selectGroupSearchFeature,
  (state: State) => state.groups
);

export const selectGroupSearchLoading = createSelector(
  selectGroupSearchFeature,
  (state: State) => state.searchGroupLoading
);

export const selectError = createSelector(
  selectGroupSearchFeature,
  (state: State) => state.error
);

export const selectCreatedGroup = createSelector(
  selectGroupSearchFeature,
  (state: State) => state.createdGroup
);
