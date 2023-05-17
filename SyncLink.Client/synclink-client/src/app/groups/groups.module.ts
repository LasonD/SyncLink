import { inject, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRouteSnapshot, RouterModule, RouterStateSnapshot, Routes } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { GroupsSearchComponent } from "./groups-search/groups-search.component";
import { CreateGroupComponent } from './create-group/create-group.component';
import { GroupHubComponent } from './group-hub/group-hub.component';
import { StoreModule } from "@ngrx/store";
import { groupsSearchReducer } from "./groups-search/store/groups-search.reducer";
import { createGroupReducer } from "./create-group/store/create-group.reducer";
import { AuthGuard } from "../auth/services/auth.guard";
import { groupHubReducer } from "./group-hub/store/group-hub.reducer";
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
    StoreModule.forFeature('groupSearch', groupsSearchReducer),
    StoreModule.forFeature('createGroup', createGroupReducer),
    StoreModule.forFeature('groupHub', groupHubReducer),
    RouterModule.forChild(routes),
    MatListModule,
    MatExpansionModule,
    MatSidenavModule,
    MatIconModule,
    FlexLayoutModule,
    RoomsModule,
    NgxColorsModule,
  ]
})
export class GroupsModule { }
