import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JoinRequestsComponent } from './join-requests/join-requests.component';
import { MatListModule } from "@angular/material/list";
import { MatButtonModule } from "@angular/material/button";
import { Routes } from "@angular/router";

export const adminRoutes: Routes = [
  {
    path: 'admin/join-requests', component: JoinRequestsComponent,
  },
];

@NgModule({
  declarations: [
    JoinRequestsComponent
  ],
  imports: [
    CommonModule,
    MatListModule,
    MatButtonModule
  ]
})
export class AdminModule { }
