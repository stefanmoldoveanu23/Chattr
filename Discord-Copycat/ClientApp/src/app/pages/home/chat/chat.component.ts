import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Log } from '../../../../data/interfaces/Log';
import { User } from '../../../../data/interfaces/user';
import { ChatService } from '../../../core/services/api/chat/chat.service';
import { UserService } from '../../../core/services/api/user/user.service';
import { SignalrService } from '../../../core/services/signalr/signalr.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  group: string = '';

  // false -> profile; true -> server
  mode: boolean = false;

  logs: Log[];
  baseLog: Log = {
    id: '',
    dateCreated: new Date(),
    senderId: '',
    message: ''
  };

  users: User[] = [];

  form;

  constructor(private readonly activatedRoute: ActivatedRoute, private readonly signalR: SignalrService, private readonly formBuilder: FormBuilder, private readonly userService: UserService, private readonly chatService: ChatService) {
    activatedRoute.params.subscribe(
      params => {
        if (params.serverId != undefined) {
          this.mode = true;
          this.chatService.getUsers(params.chatId ?? '').subscribe(
            users => this.users = users,
            error => console.error(`Error getting users in chat with id ${params.chatId ?? ''}: ` + error)
          );

          this.chatService.getLogs(params.chatId ?? '').subscribe(
            logs => this.logs = logs,
            error => console.error(`Error getting logs in chat with id ${params.chatId ?? ''}: ` + error)
          );

          this.group = (params.serverId ?? '') + (params.chatId ?? '')
        } else {
          this.userService.getSelf().subscribe(
            self => this.users.push(self),
            error => console.error(`Error getting self: ${error}.`)
          );

          this.userService.getUserById(params.chatId ?? '').subscribe(
            friend => this.users.push(friend),
            error => console.error(`Error getting friend: ${error}.`)
          );

          this.userService.getFriendship(params.chatId ?? '').subscribe(
            friendship => this.group = friendship,
            error => console.error(`Error getting friendship id: ${error}.`)
          );

          this.userService.getLogs(params.chatId ?? '').subscribe(
            logs => this.logs = logs,
            error => console.error(`Error getting logs: ${error}.`)
          );
        }

      }
    );

    signalR.startConnection(this.group);
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

  ngOnInit(): void {
  }

}
