import { Component } from '@angular/core';

@Component({
  selector: 'app-group-feed',
  templateUrl: './group-feed.component.html',
  styleUrls: ['./group-feed.component.scss']
})
export class GroupFeedComponent {
  feedItems = [
    {
      id: 1,
      type: 'discussion',
      wordPhrase: 'Learning English',
      descriptionOrQuestion: 'What are some effective ways to learn English?',
      discussionItems: [
        {
          text: 'Practicing speaking is the best way.',
          upVotesCount: 5,
          downVotesCount: 2,
          discussionId: 1,
        },
        {
          text: 'You can try language learning apps.',
          upVotesCount: 3,
          downVotesCount: 1,
          discussionId: 1,
        },
      ],
    },
    {
      id: 2,
      type: 'voting',
      question: 'Do you find learning English difficult?',
      votingOptions: [
        {
          id: 1,
          text: 'Yes',
          votes: [
            { voterId: 1 },
            { voterId: 2 },
          ],
          votingId: 2,
        },
        {
          id: 2,
          text: 'No',
          votes: [],
          votingId: 2,
        },
      ],
    },
    {
      id: 3,
      type: 'quiz',
      topic: 'English Grammar',
      question: 'What is the past tense of "go"?',
      options: [
        { text: 'Went', isCorrect: true },
        { text: 'Gone', isCorrect: false },
        { text: 'Goed', isCorrect: false },
        { text: 'Goes', isCorrect: false },
      ],
    },
  ];

  //constructor(private feedService: FeedService) { } // Inject the service

  ngOnInit() {
    console.log('Feed component')
    // this.feedService.getFeedItems().subscribe(feedItems => { // Call the service method to get feed items
    //   this.feedItems = feedItems; // Assign the response to the local property
    // });
  }
}
