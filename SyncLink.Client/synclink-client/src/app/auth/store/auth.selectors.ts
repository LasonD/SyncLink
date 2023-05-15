import { createFeatureSelector, createSelector } from "@ngrx/store";
import { AuthState } from "./auth.reducer";

export const selectAuthFeature = createFeatureSelector<AuthState>('auth');

export const selectAuthToken = createSelector(
  selectAuthFeature,
  (state: AuthState) => state.user?.token
);

export const selectUserId = createSelector(
  selectAuthFeature,
  (state: AuthState) => state.user?.userId
);

