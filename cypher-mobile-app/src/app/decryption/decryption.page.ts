import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-decryption',
  templateUrl: './decryption.page.html',
  styleUrls: ['./decryption.page.scss'],
})
export class DecryptionPage implements OnInit {

  constructor(public router : Router) { }

  ngOnInit() {
  }

  goToMapscreen() {
    this.router.navigate(['map-screen']);
  }
}
