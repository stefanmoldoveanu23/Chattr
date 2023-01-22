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
      .withUrl('https://localhost:7291/chatHub', {
        accessTokenFactory: () => (localStorage.getItem('token') ?? '')
      })
      .build();

    this.group = group;

    this.hubConnection
      .start()
      .then((data: any) => {
        this.hubConnection.invoke('JoinGroup', this.group)
          .catch((error: any) => {
            console.log('Error entering group ' + this.group);
            return;
          })
        console.log('Connection started!');
      })
      .catch((error: any) => console.log('Error while starting connection: ' + error));
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
