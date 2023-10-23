import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Log } from '../../../../data/interfaces/log';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  messageReceived = new EventEmitter<Log>();
  messageDeleted = new EventEmitter<string>();

  group !: String;
  private hubConnection !: signalR.HubConnection;

  public startConnection(group: String) {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7291/chatHub')
      .build();

    this.group = group;

    this.hubConnection
      .start()
      .then(() => {
        this.hubConnection.invoke('JoinGroup', this.group)
          .then(() => {
            console.log(`Connection started to group ${this.group}!`);
          })
          .catch((error: any) => {
            console.log(`Error entering group ${this.group}: ` + error);
            return;
          })
      })
      .catch((error: any) => console.log(`Error while starting connection to group ${this.group}: ` + error));
  }

  public setupSignals() {
    this.hubConnection.on('ReceiveMessage', (data: any) => {
      console.log('Data received.');
      this.messageReceived.emit(data);
    });

    this.hubConnection.on('DeleteMessage', (data: any) => {
      console.log('Message deleted.');
      this.messageDeleted.emit(data);
    })
  }

  public sendMessage(data: Log) {
    this.hubConnection.invoke('SendMessage', data, this.group)
      .then(() => console.log('Message sent succesfully.'))
      .catch((error: any) => {
        console.log('An error has occured sending message.')
        console.error(error);
      })
  }

  public deleteMessage(logId: string) {
    this.hubConnection.invoke('DeleteMessage', logId, this.group)
      .then(() => console.log('Deletion request sent succesfully.'))
      .catch((error: any) => {
        console.log('An error has occured deleting message.');
        console.error(error);
      })
  }

  constructor() { }
}
