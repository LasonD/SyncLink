export interface Message {
  id: number;
  editedDateTime: Date;
  creationDate: Date;
  isEdited: boolean;
  text: string | null;
  senderId: number;
  roomId: number;
}
