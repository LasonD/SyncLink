import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WhiteboardComponent } from './whiteboard/whiteboard.component';
import { NgWhiteboardModule } from "ng-whiteboard";
import { Routes } from "@angular/router";

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
    NgWhiteboardModule
  ]
})
export class FeaturesModule { }
