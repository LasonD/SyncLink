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
}


