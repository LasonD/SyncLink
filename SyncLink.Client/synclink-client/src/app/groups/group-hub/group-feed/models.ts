import { Group } from "../../../models/group.model";
import { User } from "../../../auth/user.model";

export interface FeedItem {
  type: FeedItemType;
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

export interface Voting extends FeedItem {
  type: FeedItemType.Voting;
  question: string;
  votingOptions: VotingOption[];
}

export interface VotingOption {
  text: string;
  votes: Vote[];
  votingId: number;
  voting: Voting;
}

export interface Vote {
  voterId: number;
  voter: User;
}

export interface WordPhraseOfDayDiscussion extends FeedItem {
  type: FeedItemType.Discussion;
  wordPhrase: string;
  descriptionOrQuestion?: string;
  discussionItems: DiscussionItem[];
}

export interface DiscussionItem {
  text: string;
  upVotesCount: number;
  downVotesCount: number;
  discussionId: number;
  discussion: WordPhraseOfDayDiscussion;
}

export interface WordsQuiz extends FeedItem {
  type: FeedItemType.WordsQuiz;
  topic: string;
  question: string;
  options: QuizOption[];
}

export interface QuizOption {
  text: string;
  isCorrect: boolean;
}
