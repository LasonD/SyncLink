export class Message {
  constructor(source: Message) {
    this.id = source.id;
    this.editedDateTime = new Date(source.editedDateTime);
    this.creationDate = new Date(source.creationDate);
    this.text = source.text;
    this.senderId = source.senderId;
    this.roomId = source.roomId;
    this.groupId = source.groupId;
  }

  id: number;
  editedDateTime: Date;
  creationDate: Date;
  isEdited: boolean;
  text: string | null;
  senderId: number;
  roomId: number;
  groupId: number;
}
