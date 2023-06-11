import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JoinRequestsComponent } from './join-requests/join-requests.component';
import { MatListModule } from "@angular/material/list";

@NgModule({
  declarations: [
    JoinRequestsComponent
  ],
  imports: [
    CommonModule,
    MatListModule
  ]
})
export class AdminModule { }
