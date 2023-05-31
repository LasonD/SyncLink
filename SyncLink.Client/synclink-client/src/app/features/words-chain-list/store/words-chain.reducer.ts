import { Page } from "../../../models/pagination.model";

export interface WordsChainOverview {
  id: number,
}

export interface WordsChain {
  id: number,
  topic: string,
  words: Page<WordChainEntry>,
  participants: WordsChainParticipant[],
}

export interface WordChainEntry {
  word: string,
  senderId: number,
}

export interface WordsChainParticipant {
  id: number,
  username: string,
  score: number,
}
