import { createAction, props } from "@ngrx/store";
import { Page } from "../../../models/pagination.model";
import { GroupJoinRequest, UpdateGroupJonRequest } from "./join-requests.reducer";

export const getGroupJoinRequests = createAction(
  '[Admin Join Requests] Get Group Join Requests',
  props<{ groupId: number }>()
)

export const getGroupJoinRequestsSuccess = createAction(
  '[Admin Join Requests] Get Group Join Requests Success',
  props<{ joinRequests: Page<GroupJoinRequest> }>()
)

export const getGroupJoinRequestsFailure = createAction(
  '[Admin Join Requests] Get Group Join Requests Failure',
  props<{ error: any }>()
)

export const updateGroupJoinRequest = createAction(
  '[Admin Join Requests] Update Group Join Request',
  props<{ groupId: number, joinRequestId: number, updatedState: UpdateGroupJonRequest }>()
)

export const updateGroupJoinRequestSuccess = createAction(
  '[Admin Join Requests] Update Group Join Request Success',
  props<{ updatedRequest: GroupJoinRequest }>()
)

export const updateGroupJoinRequestFailure = createAction(
  '[Admin Join Requests] Update Group Join Request Failure',
  props<{ error: any }>()
)
