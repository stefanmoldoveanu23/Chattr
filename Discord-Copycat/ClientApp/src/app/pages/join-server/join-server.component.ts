import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Server } from '../../../data/interfaces/server';
import { ServerService } from '../../core/services/api/server/server.service';

@Component({
  selector: 'app-join-server',
  templateUrl: './join-server.component.html',
  styleUrls: ['./join-server.component.css']
})
export class JoinServerComponent implements OnInit {

  server: Server = { id: '', description: '', name: '' };

  constructor(public readonly activatedRoute: ActivatedRoute, public readonly serverService: ServerService) {
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

  ngOnInit(): void {
  }

}
