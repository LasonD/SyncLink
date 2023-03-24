import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { GroupsSearchComponent } from "./groups-search.component";

const routes: Routes = [{ path: '', component: GroupsSearchComponent, },];

@NgModule({
  declarations: [
    GroupsSearchComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
  ]
})
export class GroupsSearchModule { }
