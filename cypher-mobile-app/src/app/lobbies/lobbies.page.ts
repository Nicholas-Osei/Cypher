import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../Services/api.service';
import { render } from 'creditcardpayments/creditCardPayments';
import { LoginService } from '../Services/login.service';
@Component({
  selector: 'app-lobbies',
  templateUrl: './lobbies.page.html',
  styleUrls: ['./lobbies.page.scss'],
})
export class LobbiesPage implements OnInit {
  mybalance = 0;
  moneyToPay = 10;
  checkbalance = false;
  gamePrice = 10;
  checkPage = '';
  games: any;
  id: any;
  showInput = false;

  playerbyId: any;
  constructor(private router: Router, public lobbyName: ApiService, public loginservice: LoginService) {

  }

  ngOnInit() {
    this.id = localStorage.getItem('Id');
    console.log(this.id);
    this.getPlayerbyId(this.id);
  }

  getPlayerbyId(pId) {
    this.lobbyName.getPlayerById(pId).subscribe(
      u => {
        console.log('Got ID'); this.playerbyId = u;
        this.mybalance = this.playerbyId.data.balance;
      });
  }

  showInputField() {
    this.showInput = true;
    this.games = document.getElementById('games');
    this.games.style.display = 'none';
  }
  addMoneyToWallet(money) {
    console.log('triggerd');
    this.checkbalance = true;
    render({
      id: '#myPaypalButtons',
      currency: 'EUR',
      value: money.toString(),
      onApprove: (details) => {
        alert('Transaction succesfull');
        this.mybalance += money;
        this.lobbyName.updatePlayer(
          {
            id: this.id,
            balance: this.mybalance
          }, this.id
        ).subscribe(() => { this.ngOnInit(); console.log('money updated'); });
        this.games.style.display = 'block';
        document.getElementById('myPaypalButtons').style.display = 'none';
        this.showInput = false;
      }
    });
  }

  // eslint-disable-next-line @typescript-eslint/naming-convention
  GoTo(page: string, lobbiesName: string) {
    // this.checkbalance=true;
    this.checkPage = 'lobbies';
    this.games = document.getElementById('games');
    this.lobbyName.lobbyNaam = lobbiesName;
    localStorage.setItem('LobbyName', lobbiesName);
    console.log('Called open ' + page);
    console.log(lobbiesName);
    if (lobbiesName === 'The Flash') {
      this.gamePrice = 20;
      this.moneyToPay = this.gamePrice - this.mybalance;
    }
    else {
      this.moneyToPay = 10;
      this.gamePrice = 10;
    }
    if (this.mybalance >= this.gamePrice) {
      console.log(this.gamePrice);
      this.mybalance -= this.gamePrice;
      console.log(this.mybalance);
      this.lobbyName.updatePlayer(
        {
          id: this.id,
          balance: this.mybalance
        }, this.id
      ).subscribe(() => { this.ngOnInit(); console.log('money updated'); });
      this.router.navigate([page]);
    } else {
      const buyRequest = confirm('You do not have enough balance to start this game. By credits ?');
      console.log(this.gamePrice);
      if (buyRequest) {

        this.games.style.display = 'none';
        this.checkbalance = true;
        // this.moneyToPay = this.gamePrice - this.mybalance;
        render({
          id: '#myPaypalButtons',
          currency: 'EUR',
          value: this.moneyToPay.toString(),
          onApprove: (details) => {
            alert('Transaction succesfull');
            this.mybalance += this.moneyToPay;
            this.lobbyName.updatePlayer(
              {
                id: this.id,
                balance: this.mybalance
              }, this.id
            ).subscribe(() => { this.ngOnInit(); console.log('money updated'); });
            this.games.style.display = 'block';
            document.getElementById('myPaypalButtons').style.display = 'none';
            this.moneyToPay = 10;
          }
        });
      } else {
        console.log('cancelled');
        this.games.style.display = 'block';
      }

    }
    // this.router.navigate([page]);
  }

  switchToPage(page, lobbiesName: string) {
    this.router.navigate([page]);
  }
  goBack(page: string) {
    if (this.checkPage === 'lobbies') {
      // this.checkbalance = false;
      // document.getElementById('myPaypalButtons').style.display = 'none';
      this.games.style.display = 'block';
      console.log('to lobbies');
      window.location.reload();
    } else {
      this.router.navigate([page]);
    }
  }
}
