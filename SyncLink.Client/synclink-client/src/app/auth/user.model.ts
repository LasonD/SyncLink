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
      this.expirationDate = new Date(new Date().getTime() + expiresIn * 1000);
    } else {
      this.expirationDate = new Date(expirationDate);
    }
  }

  get token() {
    console.log('Token is invalid: ', this.expirationDate, new Date().getTime() > this.expirationDate.getTime());
    if (!this.expirationDate || new Date().getTime() > this.expirationDate.getTime()) {
      console.log('Token is invalid: ', this.expirationDate, new Date().getTime() > this.expirationDate.getTime());
      return null;
    }

    return this._token;
  }
}
