import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-voting-feed-item',
  templateUrl: './voting-feed-item.component.html',
  styleUrls: ['./voting-feed-item.component.scss']
})
export class VotingFeedItemComponent {
  @Input() item: any;
}
