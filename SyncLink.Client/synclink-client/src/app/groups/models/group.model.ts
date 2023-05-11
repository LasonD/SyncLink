export interface GroupComplete extends GroupOverview {
  members: GroupMember[];
}

export interface Group extends GroupOverview {
  members: GroupMember,
  rooms: Room,
}

export interface GroupOverview {
  id: number;
  name: string;
  description?: string;
  isPrivate: boolean;
  membersCount: number;
}

export enum GroupSearchMode {
  Membership = 'Membership',
  ExplorePublic = 'ExplorePublic',
  Owned = 'Owned',
}

export interface CreateGroupDto {
  name: string,
  description: string,
  isPrivate: boolean,
}

export interface GroupMember {
  identityId: string;
  userId: number;
  username: string | null;
  firstName: string;
  lastName: string;
  email: string | null;
}

export interface Room {

}



