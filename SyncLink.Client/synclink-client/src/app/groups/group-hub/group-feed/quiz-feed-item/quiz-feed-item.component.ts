import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-quiz-feed-item',
  templateUrl: './quiz-feed-item.component.html',
  styleUrls: ['./quiz-feed-item.component.scss']
})
export class QuizFeedItemComponent {
  @Input() item: any;
}
