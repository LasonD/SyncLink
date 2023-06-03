import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-quiz-feed-item',
  templateUrl: './quiz-feed-item.component.html',
  styleUrls: ['./quiz-feed-item.component.scss']
})
export class QuizFeedItemComponent {
  @Input() item: any;

  selectedOption: string;

  submitVote() {
    if (this.selectedOption) {
      // Submit the vote.
    } else {
      // Show an error message because no option was selected.
    }
  }

}
