import { createFeatureSelector, createSelector } from '@ngrx/store';
import { State } from './groups.reducer';

export const selectGroupSearchFeature = createFeatureSelector<State>('groupSearch');

export const selectGroups = createSelector(
  selectGroupSearchFeature,
  (state: State) => state.groups
);

export const selectLoading = createSelector(
  selectGroupSearchFeature,
  (state: State) => state.loading
);

export const selectError = createSelector(
  selectGroupSearchFeature,
  (state: State) => state.error
);
