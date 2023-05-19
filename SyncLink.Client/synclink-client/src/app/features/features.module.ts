import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WhiteboardComponent } from './whiteboard/whiteboard.component';
import { NgWhiteboardModule } from "ng-whiteboard";
import { Routes } from "@angular/router";
import { NgxColorsModule } from "ngx-colors";
import { WhiteboardsListComponent } from "./whiteboard/whiteboards-list/whiteboards-list.component";
import { StoreModule } from "@ngrx/store";
import { whiteboardReducer } from "./whiteboard/store/whiteboard.reducer";
import { MatRippleModule } from "@angular/material/core";

export const featureRoutes: Routes = [
  {
    path: 'features/whiteboards/:whiteboardId', component: WhiteboardComponent,
  },
  {
    path: 'features/whiteboards', component: WhiteboardsListComponent,
  },
];

@NgModule({
  declarations: [
    WhiteboardComponent
  ],
  imports: [
    CommonModule,
    NgWhiteboardModule,
    NgxColorsModule,
    StoreModule.forFeature('whiteboards', whiteboardReducer),
  ],
})
export class FeaturesModule { }
