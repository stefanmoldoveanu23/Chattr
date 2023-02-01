import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Log } from '../../../../data/interfaces/Log';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  messageReceived = new EventEmitter<Log>();

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

  public receiveMessage() {
    this.hubConnection.on('ReceiveMessage', (data: any) => {
      console.log('Data received.');
      this.messageReceived.emit(data);
      console.log(data);
    })
  }

  public sendMessage(data: Log) {
    this.hubConnection.invoke('SendMessage', data, this.group)
      .then(() => console.log('Message sent succesfully.'))
      .catch((error: any) => {
        console.log('An error has occured sending message ' + error);
      })
  }

  constructor() { }
}
