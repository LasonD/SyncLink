import * as fromAuth from "../auth/store/auth.reducer";
import * as fromGroups from "../groups/store/groups.reducer";
import * as fromRooms from "../rooms/store/rooms.reducer";
import * as fromFeatures from "../features/store/features.reducer";
import * as fromAdmin from "../admin/store/admin.reducer";
import { ActionReducerMap } from "@ngrx/store";

export interface AppState {
  auth: fromAuth.AuthState,
  groups: fromGroups.GroupsState,
  rooms: fromRooms.RoomsState,
  features: fromFeatures.FeaturesState,
  admin: fromAdmin.AdminState
}

export const appReducer: ActionReducerMap<AppState> = {
  auth: fromAuth.authReducer,
  groups: fromGroups.groupsReducer,
  rooms: fromRooms.roomsReducer,
  features: fromFeatures.featuresReducer,
  admin: fromAdmin.adminReducer,
};
