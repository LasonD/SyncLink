import { Component } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-group-features-list',
  templateUrl: './group-features-list.component.html',
  styleUrls: ['./group-features-list.component.scss']
})
export class GroupFeaturesListComponent {
  features: Feature[] = [ { name: 'Whiteboard', path: ['features', 'whiteboard'] } ];

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  navigateFeature(feature: Feature) {
    console.log('Navigating to', feature);
    this.router.navigate(feature.path, { relativeTo: this.activatedRoute });
  }
}

interface Feature {
  name: string;
  path: string[];
}
