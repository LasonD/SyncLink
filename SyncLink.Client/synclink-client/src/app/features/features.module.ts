import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WhiteboardComponent } from './whiteboard/whiteboard.component';
import { NgWhiteboardModule } from "ng-whiteboard";
import { Routes } from "@angular/router";
import { NgxColorsModule } from "ngx-colors";

export const featureRoutes: Routes = [
  {
    path: 'features/whiteboard', component: WhiteboardComponent,
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
