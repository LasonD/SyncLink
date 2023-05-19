import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WhiteboardComponent } from './whiteboard/whiteboard.component';
import { NgWhiteboardModule } from "ng-whiteboard";
import { Routes } from "@angular/router";
import { NgxColorsModule } from "ngx-colors";
import { WhiteboardsListComponent } from "./whiteboard/whiteboards-list/whiteboards-list.component";
import { StoreModule } from "@ngrx/store";
import { whiteboardReducer } from "./whiteboard/store/whiteboard.reducer";
import { CreateWhiteboardComponent } from './whiteboard/create-whiteboard/create-whiteboard.component';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatCardModule } from "@angular/material/card";
import { ReactiveFormsModule } from "@angular/forms";

export const featureRoutes: Routes = [
  {
    path: 'features/whiteboards/create', component: WhiteboardsListComponent,
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
    StoreModule.forFeature('whiteboards', whiteboardReducer),
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    ReactiveFormsModule,
  ],
})
export class FeaturesModule { }
