export interface GroupComplete extends Group {
  members: Member[];
}

export interface Group {
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

export interface Member {
  identityId: string;
  userId: number;
  username: string | null;
  firstName: string;
  lastName: string;
  email: string | null;
}

export interface Room {

}



