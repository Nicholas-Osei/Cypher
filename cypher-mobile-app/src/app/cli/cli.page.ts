import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cli',
  templateUrl: './cli.page.html',
  styleUrls: ['./cli.page.scss'],
})
export class CliPage implements OnInit {

  constructor(public router: Router) { }

  ngOnInit() {
  }

  goToMapscreen() {
    this.router.navigate(['map-screen']);
  }

}
