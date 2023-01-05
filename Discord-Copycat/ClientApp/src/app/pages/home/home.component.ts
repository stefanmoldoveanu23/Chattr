import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Log } from '../../../data/interfaces/Log';
import { SignalrService } from '../../core/services/signalr/signalr.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  logs: Log[];
  baseLog: Log = {
    id: '',
    dateCreated: new Date(),
    senderId: '',
    message: ''
  };

  form;

  constructor(private formBuilder: FormBuilder, private signalR: SignalrService) {
    signalR.startConnection('sampleGroup');
    signalR.receiveMessage();
    this.logs = [];
    this.subscribeToEvents();

    this.form = formBuilder.group(this.baseLog);
  }

  subscribeToEvents() {
    this.signalR.messageReceived.subscribe((data: Log) => {
      this.logs.push(data);
    });
  }

  onSendMessage() {
    var msg = this.form.get('message');
    var message = msg && msg.value ? msg.value : '';

    var date = new Date();

    this.baseLog = { id: '00000000-0000-4000-0000-000000000000', dateCreated: date, senderId: '00000000-0000-4000-0000-000000000000', message: message };

    this.signalR.sendMessage(this.baseLog);
  }
}
