import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-discussion-feed-item',
  templateUrl: './discussion-feed-item.component.html',
  styleUrls: ['./discussion-feed-item.component.scss']
})
export class DiscussionFeedItemComponent {
  @Input() item: any;

  onSubmit(value: any) {

  }
}
