export class User {
  constructor(public id: string,
              public userName: string,
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
    if (!this.expirationDate || new Date().getTime() > this.expirationDate.getTime()) {
      return null;
    }

    return this._token;
  }
}

export interface UserModel {
  expirationDate: Date,
  id: string,
  userName: string,
  email: string,
  _token: string,
  expiresIn: number,
}
