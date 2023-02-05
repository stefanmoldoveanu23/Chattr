import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Roles } from '../../../data/enums/roles';
import { Server } from '../../../data/interfaces/server';
import { ServerService } from '../../core/services/api/server/server.service';
import { UserService } from '../../core/services/api/user/user.service';

@Component({
  selector: 'app-join-server',
  templateUrl: './join-server.component.html',
  styleUrls: ['./join-server.component.css']
})
export class JoinServerComponent implements OnInit {

  server: Server = { id: '', description: '', name: '' };

  constructor(public readonly router: Router, public readonly activatedRoute: ActivatedRoute, public readonly serverService: ServerService, public readonly userService: UserService) {
    this.activatedRoute.params.subscribe(
      params => {
        if (params.token != undefined) {
          this.serverService.getServerFromToken(params.token ?? '').subscribe(
            server => this.server = server,
            error => {
              console.log("Error getting server info.");
              console.error(error);
            }
          );
        }
      }
    );
  }

  onSubmit(state: boolean) {
    if (state) {
      this.userService.joinServer(this.server.id, Roles.user).subscribe(
        () => { },
        error => {
          console.log("Error joining server.");
          console.error(error);
        }
      );
    }

    this.router.navigate([`../../home/${this.server.id}`], { relativeTo: this.activatedRoute })
      .then(() => window.location.reload);
  }

  ngOnInit(): void {
  }

}
