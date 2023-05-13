import { inject, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoomComponent } from './room/room.component';
import { CreateRoomComponent } from './create-room/create-room.component';
import { ActivatedRouteSnapshot, RouterModule, RouterStateSnapshot, Routes } from "@angular/router";
import { AuthGuard } from "../auth/services/auth.guard";
import { CreateGroupComponent } from "../groups/create-group/create-group.component";
import { MatCardModule } from "@angular/material/card";

const routes: Routes = [
  {
    path: 'create', component: CreateGroupComponent, canActivate: [(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuard).canActivate(route, state)]
  },
];

@NgModule({
  declarations: [
    RoomComponent,
    CreateRoomComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatCardModule,
  ]
})
export class RoomsModule { }
