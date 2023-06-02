import { inject, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRouteSnapshot, RouterModule, RouterStateSnapshot, Routes } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { GroupsSearchComponent } from "./groups-search/groups-search.component";
import { CreateGroupComponent } from './create-group/create-group.component';
import { GroupHubComponent } from './group-hub/group-hub.component';
import { AuthGuard } from "../auth/services/auth.guard";
import { MatListModule } from "@angular/material/list";
import { MatExpansionModule } from "@angular/material/expansion";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatIconModule } from "@angular/material/icon";
import { GroupUsersListComponent } from './group-hub/group-users-list/group-users-list.component';
import { GroupFeaturesListComponent } from './group-hub/group-features-list/group-features-list.component';
import { FlexLayoutModule } from "@angular/flex-layout";
import { RoomsModule, roomsRoutes } from "../rooms/rooms.module";
import { GroupRoomsListComponent } from "./group-hub/group-rooms-list/group-rooms-list.component";
import { featureRoutes } from "../features/features.module";
import { NgxColorsModule } from "ngx-colors";
import { NgWhiteboardModule } from "ng-whiteboard";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatButtonModule } from "@angular/material/button";

const routes: Routes = [
  {
    path: '', component: GroupsSearchComponent, canActivate: [(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuard).canActivate(route, state)],
  },
  {
    path: 'create', component: CreateGroupComponent, canActivate: [(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuard).canActivate(route, state)]
  },
  {
    path: ':groupId/hub', component: GroupHubComponent, canActivate: [(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuard).canActivate(route, state)],
    children: [...roomsRoutes, ...featureRoutes]
  },
];

@NgModule({
  declarations: [
    GroupsSearchComponent,
    CreateGroupComponent,
    GroupHubComponent,
    GroupUsersListComponent,
    GroupRoomsListComponent,
    GroupFeaturesListComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatListModule,
    MatExpansionModule,
    MatSidenavModule,
    MatIconModule,
    FlexLayoutModule,
    RoomsModule,
    NgxColorsModule,
    NgWhiteboardModule,
    MatProgressSpinnerModule,
    MatButtonModule,
  ]
})
export class GroupsModule { }
