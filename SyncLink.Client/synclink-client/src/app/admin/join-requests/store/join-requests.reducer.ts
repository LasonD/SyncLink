import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";
import { createReducer, on } from "@ngrx/store";
import { getGroupJoinRequestsSuccess } from "./join-requests.actions";
import { Page } from "../../../models/pagination.model";

export const joinRequestsAdapter: EntityAdapter<GroupJoinRequest> = createEntityAdapter<GroupJoinRequest>();

export interface JoinRequestsState extends EntityState<GroupJoinRequest> {
  lastPage: Page<GroupJoinRequest>
}

export const initialState: JoinRequestsState = joinRequestsAdapter.getInitialState({
  lastPage: null,
});

export const groupsJoinRequestsReducer = createReducer(
  initialState,
  on(getGroupJoinRequestsSuccess, (state, action)  => {
    return joinRequestsAdapter.upsertMany(action?.joinRequests?.entities, {...state, lastPage: action.joinRequests});
  }),
);

export interface SendGroupJoinRequest {
  message: string,
}

export interface GroupJoinRequest {
  message: string,
  userId: number,
  groupId: number,
  status: GroupJoinRequestStatus,
  id: number,
}

export interface UpdateGroupJonRequest {
  newState: GroupJoinRequestStatus,
}

export enum GroupJoinRequestStatus {
  Pending = 0,
  Accepted = 1,
  Rejected = 2,
}

