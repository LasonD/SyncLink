import { Component } from '@angular/core';

@Component({
  selector: 'app-group-users-list',
  templateUrl: './group-users-list.component.html',
  styleUrls: ['./group-users-list.component.scss']
})
export class GroupUsersListComponent {
  members = ['Member 1', 'Member 2', 'Member 3'];
}
