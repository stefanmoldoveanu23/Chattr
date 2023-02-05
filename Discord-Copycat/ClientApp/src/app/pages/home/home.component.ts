import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Roles } from '../../../data/enums/roles';
import { Server } from '../../../data/interfaces/server';
import { ServerService } from '../../core/services/api/server/server.service';
import { UserService } from '../../core/services/api/user/user.service';
import { CreateServerDialogComponent } from './create-server-dialog/create-server-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  servers: Server[] = [];

  constructor(private readonly route: ActivatedRoute, private readonly router: Router, private readonly matDialog: MatDialog, private readonly userService: UserService, private readonly serverService: ServerService) {
    this.userService.getServers().subscribe(
      servers => this.servers = servers,
      error => console.error(error)
    );
  }

  openCreateServer() {
    const dialogRef = this.matDialog.open(CreateServerDialogComponent, {
      width: '250px',
      enterAnimationDuration: '0.5s',
    });

    dialogRef.afterClosed().subscribe(
      form => {
        if (form != null) {
          this.serverService.create(form.data).subscribe(
            serverResponse => {
              this.userService.joinServer(serverResponse.id, Roles.admin).subscribe(
                () => window.location.reload(),
                error => {
                  console.log('Error joining creating server.');
                  console.error(error);
                }
              )
            },
            error => {
              console.log('Error creating server.');
              console.error(error);
            }
          )
        }
      },
      error => console.error(error)
    );
  }

  onSetServer(id: string) {
    this.router.navigate([`${id}`], { relativeTo: this.route });
  }
}
