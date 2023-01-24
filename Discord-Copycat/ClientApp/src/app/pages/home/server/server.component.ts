import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-server',
  templateUrl: './server.component.html',
  styleUrls: ['./server.component.css']
})
export class ServerComponent implements OnInit {
  serverId: string|null = "";

  constructor(public readonly route: ActivatedRoute) {
    this.route.params.subscribe(
      params => this.serverId = params.serverId
    );
  }

  ngOnInit(): void {
  }

}
