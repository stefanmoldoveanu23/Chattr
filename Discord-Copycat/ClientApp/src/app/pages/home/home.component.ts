import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { roles } from '../../../data/enums/roles';
import { Log } from '../../../data/interfaces/Log';
import { Server } from '../../../data/interfaces/server';
import { ServerService } from '../../core/services/api/server/server.service';
import { UserService } from '../../core/services/api/user/user.service';
import { SignalrService } from '../../core/services/signalr/signalr.service';
import { CreateServerDialogComponent } from './create-server-dialog/create-server-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  servers: Server[] = [];

  logs: Log[];
  baseLog: Log = {
    id: '',
    dateCreated: new Date(),
    senderId: '',
    message: ''
  };

  form;

  constructor(private readonly route: ActivatedRoute, private readonly router: Router, private readonly matDialog: MatDialog, private readonly formBuilder: FormBuilder, private readonly signalR: SignalrService, private readonly userService: UserService, private readonly serverService: ServerService) {
    signalR.startConnection('sampleGroup');
    signalR.receiveMessage();
    this.logs = [];
    this.subscribeToEvents();

    this.form = formBuilder.group(this.baseLog);

    this.userService.getServers().subscribe(
      servers => this.servers = servers,
      error => console.error(error)
    );

    console.log(this.servers);
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

  openCreateServer() {
    const dialogRef = this.matDialog.open(CreateServerDialogComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe(
      form => {
        this.serverService.create(form.data).subscribe(
          serverResponse => {
            this.userService.joinServer(serverResponse.id, roles.admin).subscribe(
              ok => window.location.reload(),
              error => console.log('Error joining creating server: ' + error)
            )
          },
          error => console.log('Error creating server: ' + error)
        )
      },
      error => console.log(error)
    );
  }

  onSetServer(id: string) {
    this.router.navigate([`${id}`], { relativeTo: this.route });
  }
}
