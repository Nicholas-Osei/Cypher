import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-worm-in-the-system',
  templateUrl: './worm-in-the-system.page.html',
  styleUrls: ['./worm-in-the-system.page.scss'],
})
export class WormInTheSystemPage implements OnInit {

  constructor(public router : Router) { }

  ngOnInit() {
  }

  goToMapscreen() {
    this.router.navigate(['map-screen']);
  }
}
