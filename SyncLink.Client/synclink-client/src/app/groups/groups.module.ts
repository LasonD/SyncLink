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

const routes: Routes = [
  {
    path: '', component: GroupsSearchComponent, canActivate: [(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuard).canActivate(route, state)], children: [
      { path: 'create', component: CreateGroupComponent, },
      { path: ':id/hub', component: GroupHubComponent, },
    ]
  },
];

@NgModule({
  declarations: [
    GroupsSearchComponent,
    CreateGroupComponent,
    GroupHubComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forFeature('groupSearch', groupsSearchReducer),
    StoreModule.forFeature('createGroup', createGroupReducer),
    RouterModule.forChild(routes),
  ]
})
export class GroupsModule { }
