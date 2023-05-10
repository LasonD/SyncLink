export interface AuthResult {
  identityId: string;
  userId: number;
  username: string | null;
  firstName: string;
  lastName: string;
  email: string | null;
  accessToken: string;
  expiresIn: number;
}

export class User {
  constructor(public identityId: string,
              public userId: number,
              public username: string | null,
              public firstname: string,
              public lastname: string,
              public email: string,
              private _token: string,
              private expiresIn: number = null,
              private readonly expirationDate: Date = null) {
    if (expiresIn) {
      console.log('Expires in is null', )
      this.expirationDate = new Date(new Date().getTime() + expiresIn);
    } else {
      this.expirationDate = new Date(expirationDate);
    }
  }

  get token() {
    if (!this.expirationDate || new Date().getTime() > this.expirationDate.getTime()) {
      return null;
    }

    return this._token;
  }
}

export interface UserModel {
  expirationDate: Date | null,
  userId: number,
  identityId: string,
  username: string | null,
  firstname: string,
  lastname: string,
  email: string,
  _token?: string | null,
  expiresIn: number | null,
}
