import { Component } from '@angular/core';

@Component({
  selector: 'app-group-features-list',
  templateUrl: './group-features-list.component.html',
  styleUrls: ['./group-features-list.component.scss']
})
export class GroupFeaturesListComponent {
  features: Feature[] = [ { name: 'Whiteboard' } ];
}

interface Feature {
  name: string;
}
