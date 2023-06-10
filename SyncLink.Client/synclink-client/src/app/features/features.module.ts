import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WhiteboardComponent } from './whiteboard/whiteboard.component';
import { NgWhiteboardModule } from "ng-whiteboard";
import { Routes } from "@angular/router";
import { NgxColorsModule } from "ngx-colors";
import { WhiteboardsListComponent } from "./whiteboard/whiteboards-list/whiteboards-list.component";
import { CreateWhiteboardComponent } from './whiteboard/create-whiteboard/create-whiteboard.component';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatCardModule } from "@angular/material/card";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { TextPlotGameComponent } from './text-plot-game/text-plot-game.component';
import { WordsChainComponent } from './words-chain-list/words-chain/words-chain.component';
import { WordsChainListComponent } from './words-chain-list/words-chain-list.component';
import { CreateWordsChainComponent } from './words-chain-list/create-words-chain/create-words-chain.component';
import { MatIconModule } from "@angular/material/icon";
import { TextPlotGamesListComponent } from './text-plot-game/text-plot-games-list/text-plot-games-list.component';
import { CreateTextPlotGameComponent } from './text-plot-game/create-text-plot-game/create-text-plot-game.component';
import { MatListModule } from "@angular/material/list";
import { VoteModalComponent } from './text-plot-game/vote-modal/vote-modal.component';
import { MatSelectModule } from "@angular/material/select";
import { MatDialogModule } from "@angular/material/dialog";

export const featureRoutes: Routes = [
  {
    path: 'features/whiteboards/create', component: CreateWhiteboardComponent,
  },
  {
    path: 'features/words-chain/create', component: CreateWordsChainComponent,
  },
  {
    path: 'features/text-plot-games/create', component: CreateTextPlotGameComponent,
  },
  {
    path: 'features/whiteboards/:whiteboardId', component: WhiteboardComponent,
  },
  {
    path: 'features/words-chain/:wordsChainId', component: WordsChainComponent,
  },
  {
    path: 'features/text-plot-games/:textPlotGameId', component: TextPlotGameComponent,
  },
  {
    path: 'features/whiteboards', component: WhiteboardsListComponent,
  },
  {
    path: 'features/words-chain', component: WordsChainListComponent,
  },
  {
    path: 'features/text-plot-games', component: TextPlotGamesListComponent,
  },
];

@NgModule({
  declarations: [
    WhiteboardComponent,
    CreateWhiteboardComponent,
    TextPlotGameComponent,
    WordsChainComponent,
    WordsChainListComponent,
    CreateWordsChainComponent,
    TextPlotGamesListComponent,
    CreateTextPlotGameComponent,
    VoteModalComponent
  ],
    imports: [
        CommonModule,
        NgWhiteboardModule,
        NgxColorsModule,
        MatFormFieldModule,
        MatInputModule,
        MatCardModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatIconModule,
        MatProgressSpinnerModule,
        MatListModule,
        MatSelectModule,
        MatDialogModule,
        FormsModule,
    ],
  providers: [

  ]
})
export class FeaturesModule { }
