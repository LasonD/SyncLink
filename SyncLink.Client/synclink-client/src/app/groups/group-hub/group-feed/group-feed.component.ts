import { Component } from '@angular/core';
import { DiscussionItem, FeedItemType, Voting, WordPhraseOfDayDiscussion, WordsQuiz } from "./models";
import { User } from "../../../auth/user.model";
import { Group } from "../../../models/group.model";

@Component({
  selector: 'app-group-feed',
  templateUrl: './group-feed.component.html',
  styleUrls: ['./group-feed.component.scss']
})
export class GroupFeedComponent {
  feedItemType = FeedItemType;
  feedItems: (Voting | WordPhraseOfDayDiscussion | WordsQuiz)[] = [
    {
      type: FeedItemType.Voting,
      groupId: 1,
      group: { /* ...group data here... */ } as Group,
      authorId: 123,
      author: { username: 'Alex'/* ...user data here... */ } as User,
      question: "Who should be the next group leader?",
      votingOptions: [
        {
          text: "Alice",
          votes: [
            {
              voterId: 456,
              voter: { /* ...user data here... */ } as User,
            },
            {
              voterId: 789,
              voter: { /* ...user data here... */ } as User,
            }
          ],
          votingId: 1,
          voting: { /* ...voting data here... */ } as Voting,
        },
        {
          text: "Bob",
          votes: [
            {
              voterId: 12,
              voter: { /* ...user data here... */ } as User,
            }
          ],
          votingId: 2,
          voting: { /* ...voting data here... */ } as Voting,
        }
      ]
    },
    {
      type: FeedItemType.Discussion,
      groupId: 2,
      group: { /* ...group data here... */ } as Group,
      authorId: 345,
      author: { /* ...user data here... */ } as User,
      wordPhrase: "Eternal Sunshine of the Spotless Mind",
      descriptionOrQuestion: "What are your thoughts on this movie?",
      discussionItems: [
        {
          text: "Looks good!",
          downVotesCount: 1,
          upVotesCount: 10,
        } as DiscussionItem,
        {
          text: "It's a good definition of words!!",
          downVotesCount: 2,
          upVotesCount: 8,
        } as DiscussionItem,
      ]
    },
    {
      type: FeedItemType.WordsQuiz,
      groupId: 3,
      group: { /* ...group data here... */ } as Group,
      authorId: 678,
      author: { /* ...user data here... */ } as User,
      topic: "English vocabulary",
      question: "What is the synonym for 'happy'?",
      options: [
        {
          text: "Sad",
          isCorrect: false,
        },
        {
          text: "Joyful",
          isCorrect: true,
        },
        {
          text: "Angry",
          isCorrect: false,
        },
        {
          text: "Bored",
          isCorrect: false,
        }
      ]
    }
  ];


  //constructor(private feedService: FeedService) { } // Inject the service

  ngOnInit() {
    console.log('Feed component')
    // this.feedService.getFeedItems().subscribe(feedItems => { // Call the service method to get feed items
    //   this.feedItems = feedItems; // Assign the response to the local property
    // });
  }
}
