import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
  whoId: string = '';

  // false -> profile; true -> server
  mode: boolean = false;

  logs: Log[] = [];

  users: User[] = [];

  form = this.formBuilder.group({
    message: ''
  });

  constructor(private readonly router: Router, private readonly activatedRoute: ActivatedRoute, private readonly signalR: SignalrService, private readonly formBuilder: FormBuilder, private readonly userService: UserService, private readonly chatService: ChatService) {
    activatedRoute.params.subscribe(
      params => {
        if (params.serverId != undefined) {
          this.mode = true;
          this.chatService.getUsers(params.chatId ?? '').subscribe(
            users => {
              this.users = users;

              this.chatService.getLogs(params.chatId ?? '').subscribe(
                logs => {
                  this.logs = logs;
                  this.logs.forEach(log => log.senderName = this.users.find(user => user.id == log.senderId)?.username ?? '');
                },
                () => router.navigate(['..'], { relativeTo: activatedRoute })
              );

            },
            () => router.navigate(['..'], { relativeTo: activatedRoute })
          );

          this.group = (params.serverId ?? '') + (params.chatId ?? '')
        } else {
          this.userService.getSelf().subscribe(
            self => {
              this.users.push(self);

              this.userService.getUserById(params.chatId ?? '').subscribe(
                friend => {
                  this.users.push(friend);

                  this.userService.getLogs(params.chatId ?? '').subscribe(
                    logs => {
                      this.logs = logs;
                      this.logs.forEach(log => log.senderName = this.users.find(user => user.id === log.senderId)?.username ?? '');
                    },
                    () => router.navigate(['..'], { relativeTo: activatedRoute })
                  );

                },
                () => router.navigate(['..'], { relativeTo: activatedRoute })
              );

            },
            () => router.navigate(['..'], { relativeTo: activatedRoute })
          );


          this.userService.getFriendship(params.chatId ?? '').subscribe(
            friendship => this.group = friendship,
            () => router.navigate(['..'], { relativeTo: activatedRoute })
          );
        }

        this.whoId = params.chatId ?? '';

      }
    );

    signalR.startConnection(this.group);
    signalR.receiveMessage();
    this.subscribeToEvents();
  }

  subscribeToEvents() {
    this.signalR.messageReceived.subscribe((data: Log) => {
      data.senderName = this.users.find(user => user.id === data.senderId)?.username ?? '';
      this.logs.push(data);
    });
  }

  onSendMessage() {
    var msg = this.form.get('message');
    var message = msg && msg.value ? msg.value : '';

    if (this.mode) {
      this.chatService.sendMessage(this.whoId, message).subscribe(
        log => this.signalR.sendMessage(log),
        error => {
          console.error(`Error creating new log.`);
          console.error(error);
        }
      );
    } else {
      this.userService.sendMessage(this.whoId, message).subscribe(
        log => this.signalR.sendMessage(log),
        error => {
          console.error(`Error creating new log.`);
          console.error(error);
        }
      );
    }
  }

  ngOnInit(): void {
  }

}
