import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Log } from '../../../../data/interfaces/log';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  group !: String;
  data !: Log;
  private hubConnection !: signalR.HubConnection;

  public startConnection(group : String) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost::7291/chatHub')
      .build();

    this.group = group;

    this.hubConnection
      .start()
      .then((data) => {
        this.hubConnection.invoke('JoinGroup', this.group)
          .catch(error => {
            console.log('Error entering group ' + this.group);
            return;
          })
        console.log('Connection started!');
      })
      .catch(error => console.log('Error while starting connection: ' + error));
  }

  public receiveMessage() {
    this.hubConnection.on('ReceiveMessage', (data) => {
      this.data = data;
      console.log(data);
    })
  }

  public sendMessage() {
    this.hubConnection.invoke('SendMessage', (this.data, this.group))
      .then(() => console.log('Message sent succesfully.'))
      .catch(error => {
        console.log('An error has occured sending message.');
        console.log(this.data);
      })
  }

  constructor() { }
}
