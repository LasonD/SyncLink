import * as fromWhiteboard from "../whiteboard/store/whiteboard.reducer";
import { combineReducers } from "@ngrx/store";
import { whiteboardReducer } from "../whiteboard/store/whiteboard.reducer";

export interface FeaturesState {
  whiteboard: fromWhiteboard.WhiteboardState,
}

export const featuresReducer = combineReducers<FeaturesState>({
  whiteboard: whiteboardReducer,
});
