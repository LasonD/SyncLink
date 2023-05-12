export interface Message {
  id: number;
  editedDateTime: Date;
  isEdited: boolean;
  text: string | null;
  senderId: number;
  roomId: number;
}
