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
];

@NgModule({
  declarations: [
    WhiteboardComponent,
    CreateWhiteboardComponent
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
