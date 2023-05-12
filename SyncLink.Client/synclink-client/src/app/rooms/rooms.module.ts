import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoomComponent } from './room/room.component';
import { CreateRoomComponent } from './create-room/create-room.component';



@NgModule({
  declarations: [
    RoomComponent,
    CreateRoomComponent
  ],
  imports: [
    CommonModule
  ]
})
export class RoomsModule { }
