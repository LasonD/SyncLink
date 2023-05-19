import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WhiteboardComponent } from './whiteboard/whiteboard.component';
import { NgWhiteboardModule } from "ng-whiteboard";
import { Routes } from "@angular/router";
import { NgxColorsModule } from "ngx-colors";
import { WhiteboardsListComponent } from "./whiteboard/whiteboards-list/whiteboards-list.component";

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
  ],
})
export class FeaturesModule { }
