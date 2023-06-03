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
import { GroupFeedComponent } from './group-hub/group-feed/group-feed.component';
import { DiscussionFeedItemComponent } from './group-hub/group-feed/discussion-feed-item/discussion-feed-item.component';
import { VotingFeedItemComponent } from './group-hub/group-feed/voting-feed-item/voting-feed-item.component';
import { QuizFeedItemComponent } from './group-hub/group-feed/quiz-feed-item/quiz-feed-item.component';
import { MatCardModule } from "@angular/material/card";

const routes: Routes = [
  {
    path: '', component: GroupsSearchComponent, canActivate: [(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuard).canActivate(route, state)],
  },
  {
    path: 'create', component: CreateGroupComponent, canActivate: [(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuard).canActivate(route, state)]
  },
  {
    path: ':groupId/hub', component: GroupHubComponent, canActivate: [(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuard).canActivate(route, state)],
    children: [
      {
        path: '', component: GroupFeedComponent
      },
      ...roomsRoutes,
      ...featureRoutes]
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
    GroupFeedComponent,
    DiscussionFeedItemComponent,
    VotingFeedItemComponent,
    QuizFeedItemComponent,
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
    MatCardModule,
  ]
})
export class GroupsModule { }
