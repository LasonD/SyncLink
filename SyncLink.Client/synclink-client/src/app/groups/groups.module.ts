import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { GroupsSearchComponent } from "./groups-search/groups-search.component";
import { CreateGroupComponent } from './create-group/create-group.component';

const routes: Routes = [
  { path: '', component: GroupsSearchComponent, },
  { path: 'create', component: CreateGroupComponent, },
];

@NgModule({
  declarations: [
    GroupsSearchComponent,
    CreateGroupComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ]
})
export class GroupsModule { }
