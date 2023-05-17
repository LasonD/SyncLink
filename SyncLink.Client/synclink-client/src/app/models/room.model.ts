import { Message } from "./message.model";
import { Page } from "./pagination.model";

export interface Room {
  id: number;
  name: string;
  messages: Page<Message>[];
  members: Page<RoomMember>[];
}

export interface RoomMember {
  id: number;
  username: string | null;
  isAdmin: boolean;
}
