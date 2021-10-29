import { Component, OnInit } from '@angular/core';
import { LoginService } from '../Services/login.service';

@Component({
  selector: 'app-game-screen',
  templateUrl: './game-screen.page.html',
  styleUrls: ['./game-screen.page.scss'],
})
export class GameScreenPage implements OnInit {

  constructor(public loginservice: LoginService) { }

  ngOnInit() {
  }

}
