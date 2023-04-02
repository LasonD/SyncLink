import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { GroupSearchMode } from '../models/group.model';
import { selectGroupSearchLoading, selectGroupsSearchGroups } from "./store/groups-search.selectors";
import { searchGroups } from "./store/groups-search.actions";

@Component({
  selector: 'app-group-search',
  templateUrl: './groups-search.component.html',
  styleUrls: ['./groups-search.component.scss'],
})
export class GroupsSearchComponent implements OnInit {
  groups$ = this.store.select(selectGroupsSearchGroups);
  loading$ = this.store.select(selectGroupSearchLoading);
  searchQuery: string = '';
  groupSearchMode: GroupSearchMode = GroupSearchMode.Membership;
  GroupSearchMode = GroupSearchMode;
  searchFocused: boolean = false;

  constructor(private store: Store, private router: Router) {}

  ngOnInit(): void {
    this.search();
  }

  search(): void {
    this.store.dispatch(searchGroups({ searchQuery: this.searchQuery, groupSearchMode: this.groupSearchMode }));
  }

  navigateToGroup(groupId: number): void {
    this.router.navigate([`/groups/${groupId}`]);
  }

  navigateToCreateGroup(): void {
    this.router.navigate(['/groups/create']);
  }
}
