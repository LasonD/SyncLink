import { Injectable } from "@angular/core";
import { Actions } from "@ngrx/effects";
import { SignalRService } from "../../../common/services/signalr.service";

@Injectable()
export class TextPlotGameEffects {
  constructor(private actions$: Actions, private signalrService: SignalRService) {}


}
