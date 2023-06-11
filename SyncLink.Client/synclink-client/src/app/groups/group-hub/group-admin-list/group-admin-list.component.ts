import { Component } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-group-admin-list',
  templateUrl: './group-admin-list.component.html',
  styleUrls: ['./group-admin-list.component.scss']
})
export class GroupAdminListComponent {
  adminFeatures: AdminFeature[] = [
    { name: 'Join Requests', path: ['admin', 'join-requests'] },
  ];

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  navigateFeature(adminFeature: AdminFeature) {
    this.router.navigate(adminFeature.path, { relativeTo: this.activatedRoute });
  }
}

interface AdminFeature {
  name: string;
  path: string[];
}
