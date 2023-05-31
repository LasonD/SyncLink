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
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { TextPlotGameComponent } from './text-plot-game/text-plot-game.component';
import { WordsChainComponent } from './words-chain-list/words-chain/words-chain.component';
import { WordsChainListComponent } from './words-chain-list/words-chain-list.component';
import { CreateWordsChainComponent } from './words-chain-list/create-words-chain/create-words-chain.component';

export const featureRoutes: Routes = [
  {
    path: 'features/whiteboards/create', component: CreateWhiteboardComponent,
  },
  {
    path: 'features/whiteboards/:whiteboardId', component: WhiteboardComponent,
  },
  {
    path: 'features/whiteboards', component: WhiteboardsListComponent,
  },
  {
    path: 'features/words-chain', component: WordsChainComponent,
  },
];

@NgModule({
  declarations: [
    WhiteboardComponent,
    CreateWhiteboardComponent,
    TextPlotGameComponent,
    WordsChainComponent,
    WordsChainListComponent,
    CreateWordsChainComponent
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
    MatProgressSpinnerModule,
  ],
})
export class FeaturesModule { }
