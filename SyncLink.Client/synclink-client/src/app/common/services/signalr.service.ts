import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from "../../environments/environment";
import { Store } from "@ngrx/store";
import { sendMessageSuccess } from "../../rooms/store/rooms.actions";
import { AppState } from "../../store/app.reducer";
import { selectAuthToken } from "../../auth/store/auth.selectors";
import { firstValueFrom } from "rxjs";
import { filter } from "rxjs/operators";
import { Message } from "../../models/message.model";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;

  constructor(private store: Store<AppState>) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiBaseUrl}/hubs/general`, {
        withCredentials: true,
        accessTokenFactory: () => firstValueFrom(this.store.select(selectAuthToken).pipe(filter(token => !!token))),
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('messageReceived', (roomId, otherUserId, isPrivate, message: Message) => {
      this.store.dispatch(sendMessageSuccess({ roomId, otherUserId, isPrivate, message: new Message(message) }));
    });

    this.hubConnection.start()
      .then(() => console.log('SignalR connection started.'))
      .catch(err => console.log('Error while starting SignalR connection: ', err));
  }

  public groupOpened(groupId: number) {
    return this.hubConnection.invoke('groupOpened', groupId);
  }

  public groupClosed(groupId: number) {
    return this.hubConnection.invoke('groupClosed', groupId);
  }
}
