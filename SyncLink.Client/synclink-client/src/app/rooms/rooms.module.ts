import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoomComponent } from './room/room.component';
import { CreateRoomComponent } from './create-room/create-room.component';
import { Routes } from "@angular/router";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatSelectModule } from "@angular/material/select";

export const roomsRoutes: Routes = [
  {
    path: 'rooms/create', component: CreateRoomComponent,
  },
  {
    path: 'rooms/:roomId', component: RoomComponent, pathMatch: "full"
  },
  {
    path: 'members/:userId/private', component: RoomComponent, pathMatch: "full"
  },
];

@NgModule({
  declarations: [
    RoomComponent,
    CreateRoomComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
  ]
})
export class RoomsModule {
}
