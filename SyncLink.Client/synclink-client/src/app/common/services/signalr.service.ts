import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from "../../environments/environment";
import { Store } from "@ngrx/store";
import { sendMessageSuccess } from "../../rooms/store/rooms.actions";
import { AppState } from "../../store/app.reducer";
import { selectAuthToken } from "../../auth/store/auth.selectors";
import { firstValueFrom, Subject } from "rxjs";
import { filter } from "rxjs/operators";
import { Message } from "../../models/message.model";
import { WhiteboardElement } from "ng-whiteboard";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  private readonly connectionPromise: Promise<void>;

  public boardChange$: Subject<WhiteboardElement[]> = new Subject<WhiteboardElement[]>();

  constructor(private store: Store<AppState>) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiBaseUrl}/hubs/general`, {
        withCredentials: true,
        accessTokenFactory: () => firstValueFrom(this.store.select(selectAuthToken).pipe(filter(token => !!token))),
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('messageReceived', (roomId, otherUserId, isPrivate, message: Message) => {
      this.store.dispatch(sendMessageSuccess({ roomId, otherUserId, isPrivate, correlationId: null, message: new Message(message) }));
    });

    this.hubConnection.on('boardUpdated', (changes) => {
      this.boardChange$.next(JSON.parse(changes) as WhiteboardElement[]);
    });

    this.connectionPromise = this.hubConnection.start()
      .then(() => console.log('SignalR connection started.'))
      .catch(err => console.log('Error while starting SignalR connection: ', err));
  }

  public async groupOpened(groupId: number) {
    await this.connectionPromise;
    return this.hubConnection.invoke('groupOpened', groupId);
  }

  public async groupClosed(groupId: number) {
    await this.connectionPromise;
    return this.hubConnection.invoke('groupClosed', groupId);
  }

  public async whiteboardUpdated(groupId: number, id: number, change: WhiteboardElement[]) {
    await this.connectionPromise;
    return this.hubConnection.invoke('boardUpdated', groupId, id,  change);
  }
}

