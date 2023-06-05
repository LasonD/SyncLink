import { Injectable } from "@angular/core";
import { SignalRService } from "./signalr.service";
import { TextPlotEntry, TextPlotGame, TextPlotVote } from "../../features/text-plot-game/store/text-plot-game.reducer";
import { AppState } from "../../store/app.reducer";
import { Store } from "@ngrx/store";
import {
  entryCommittedExternal,
  entryVotedExternal, gameEndedExternal,
  gameStartedExternal,
  newEntryExternal
} from "../../features/text-plot-game/store/text-plot-game.actions";

@Injectable({
  providedIn: 'root'
})
export class TextPlotGameService {
  constructor(private signalrService: SignalRService, private store: Store<AppState>) {
    this.signalrService.on('gameStarted', (game: TextPlotGame) => {
      this.store.dispatch(gameStartedExternal({ game }));
    });

    this.signalrService.on('newEntry', (entry: TextPlotEntry) => {
      this.store.dispatch(newEntryExternal({ entry }));
    });

    this.signalrService.on('voteReceived', (vote: TextPlotVote) => {
      this.store.dispatch(entryVotedExternal({ vote }));
    });

    this.signalrService.on('gameEnded', (game: TextPlotGame) => {
      this.store.dispatch(gameEndedExternal({ game }));
    });

    this.signalrService.on('entryCommitted', (entry: TextPlotEntry) => {
      this.store.dispatch(entryCommittedExternal({ entry }));
    });
  }
}
