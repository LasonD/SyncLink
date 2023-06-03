import { Group } from "../../../models/group.model";
import { User } from "../../../auth/user.model";

export abstract class FeedItem {
  abstract type: FeedItemType;
  groupId: number;
  group: Group;
  authorId?: number;
  author?: User;
}

export enum FeedItemType {
  Discussion,
  Voting,
  WordsQuiz,
}

export class Voting extends FeedItem {
  type: FeedItemType.Voting;
  question: string;
  votingOptions: VotingOption[];
}

export class VotingOption {
  text: string;
  votes: Vote[];
  votingId: number;
  voting: Voting;
}

export class Vote {
  voterId: number;
  voter: User;
}

export class WordPhraseOfDayDiscussion extends FeedItem {
  type: FeedItemType.Discussion;
  wordPhrase: string;
  descriptionOrQuestion?: string;
}

export class DiscussionItem {
  text: string;
  upVotesCount: number;
  downVotesCount: number;
  discussionId: number;
  discussion: WordPhraseOfDayDiscussion;
}

export class WordsQuiz extends FeedItem {
  type: FeedItemType.WordsQuiz;
  topic: string;
  question: string;
  options: QuizOption[];
}

class QuizOption {
  text: string;
  isCorrect: boolean;
}
