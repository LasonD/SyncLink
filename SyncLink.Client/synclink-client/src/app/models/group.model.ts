export interface GroupComplete extends Group {
  members: GroupMember[];
}

export interface Group {
  id: number;
  name: string;
  description?: string;
  isPrivate: boolean;
  membersCount: number;
  isAdmin: boolean;
}

export enum GroupSearchMode {
  Membership = 'Membership',
  ExplorePublic = 'ExplorePublic',
  Owned = 'Owned',
  Private = 'Private',
}

export interface CreateGroupDto {
  name: string,
  description: string,
  isPrivate: boolean,
}

export interface GroupMember {
  id: number;
  username: string | null;
  isCreator: boolean;
  isAdmin: boolean;
  privateRoomId: number;
}



