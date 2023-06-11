import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { GroupSearchMode } from '../../models/group.model';
import { selectGroupSearchLoading, selectGroupsSearchGroups } from "./store/groups-search.selectors";
import { searchGroups, sendGroupJoinRequest } from "./store/groups-search.actions";
import { debounceTime, Subject, takeUntil } from "rxjs";

@Component({
  selector: 'app-group-search',
  templateUrl: './groups-search.component.html',
  styleUrls: ['./groups-search.component.scss'],
})
export class GroupsSearchComponent implements OnInit, OnDestroy {
  destroyed$: Subject<boolean> = new Subject<boolean>();
  groups$ = this.store.select(selectGroupsSearchGroups);
  loading$ = this.store.select(selectGroupSearchLoading);
  searchQuery: string = '';
  groupSearchMode: GroupSearchMode = GroupSearchMode.Membership;
  GroupSearchMode = GroupSearchMode;
  searchFocused: boolean = false;

  search$: Subject<string> = new Subject<string>();

  constructor(private store: Store, private router: Router) {}

  ngOnInit(): void {
    this.search$
      .pipe(
        takeUntil(this.destroyed$),
        debounceTime(300)
      ).subscribe((query) => {
      this.store.dispatch(searchGroups({ searchQuery: query, groupSearchMode: this.groupSearchMode }));
    });

    this.search();
  }

  search(): void {
    this.search$.next(this.searchQuery);
  }

  selectGroup(groupId: number): void {
    if (this.groupSearchMode === GroupSearchMode.ExplorePublic) {
      this.store.dispatch(sendGroupJoinRequest({ groupId, request: { message: '' } }))
    } else{
      this.router.navigate([`/groups/${groupId}/hub`]);
    }
  }

  navigateToCreateGroup(): void {
    this.router.navigate(['/groups/create']);
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
