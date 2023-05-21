import * as fromWhiteboard from "../whiteboard/store/whiteboard.reducer";
import { combineReducers } from "@ngrx/store";
import { whiteboardReducer } from "../whiteboard/store/whiteboard.reducer";
import { textPlotGameReducer, TextPlotGameState } from "../text-plot-game/store/text-plot-game.reducer";

export interface FeaturesState {
  whiteboards: fromWhiteboard.WhiteboardState,
  textPlotGame: TextPlotGameState,
}

export const featuresReducer = combineReducers<FeaturesState>({
  whiteboards: whiteboardReducer,
  textPlotGame: textPlotGameReducer,
});
