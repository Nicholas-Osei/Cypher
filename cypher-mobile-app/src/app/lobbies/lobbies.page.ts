import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-lobbies',
  templateUrl: './lobbies.page.html',
  styleUrls: ['./lobbies.page.scss'],
})
export class LobbiesPage implements OnInit {

  constructor(private router : Router) { }

  ngOnInit() {
  }

  GoTo(page : string){
    console.log("Called open " + page);
    this.router.navigate([page]);
  }
}
