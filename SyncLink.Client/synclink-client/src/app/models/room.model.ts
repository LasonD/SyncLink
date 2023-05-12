import { Message } from "./message.model";
import { Page } from "./pagination.model";

export interface Room {
  id: number;
  name: string;
  messages: Page<Message>;
}
