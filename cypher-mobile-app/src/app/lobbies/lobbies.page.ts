import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../Services/api.service';
@Component({
  selector: 'app-lobbies',
  templateUrl: './lobbies.page.html',
  styleUrls: ['./lobbies.page.scss'],
})
export class LobbiesPage implements OnInit {

  constructor(private router: Router, public lobbyName: ApiService) { }

  ngOnInit() {
  }

  GoTo(page: string, lobbiesName: string) {
    this.lobbyName.lobbyNaam = lobbiesName;
    localStorage.setItem('LobbyName', lobbiesName);
    console.log("Called open " + page);
    this.router.navigate([page]);
  }
}
