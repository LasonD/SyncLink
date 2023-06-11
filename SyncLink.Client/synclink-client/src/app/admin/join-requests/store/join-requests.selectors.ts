import { joinRequestsAdapter } from "./join-requests.reducer";
import { AppState } from "../../../store/app.reducer";

export const joinRequestSelectors = joinRequestsAdapter.getSelectors((state: AppState) => state.admin.joinRequests);
