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
  lobbies = [];
  teller = 0;
  alertCounter = 0;


  playerbyId: any;
  constructor(private router: Router, public lobbyName: ApiService, public loginservice: LoginService) {

  }

  ngOnInit() {
    this.id = localStorage.getItem('Id');
    console.log(this.id);
    this.getPlayerbyId(this.id);
    this.lobbyName.getAllLobbies().subscribe(l => {
      this.lobbies = l.data;
      console.log('Got all lobbies');
    });
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


    this.lobbies.forEach(element => {
      if (element.name === lobbiesName) {
        console.log(element.players.length);
        element.players.forEach(el => {
          console.log(el.name);

          if (this.loginservice.displayName === el.name) {
            console.log(this.loginservice.displayName, el.name);
            this.teller++;
            // this.moneyToPay = this.gamePrice - this.mybalance;
            if (lobbiesName === 'The Flash') {
              this.gamePrice = 20;
              this.moneyToPay = this.gamePrice - this.mybalance;
            }
            else {

              this.gamePrice = 10;
              this.moneyToPay = this.gamePrice - this.mybalance;
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
              this.alertCounter += 1;
              if (this.alertCounter === 1) {
                const buyRequest = confirm('You do not have enough balance to start this game. Buy credits ?');
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
                      this.mybalance -= this.gamePrice;
                      this.lobbyName.updatePlayer(
                        {
                          id: this.id,
                          balance: this.mybalance
                        }, this.id
                      ).subscribe(() => { this.ngOnInit(); console.log('money updated'); this.router.navigate([page]); });
                      this.games.style.display = 'block';
                      document.getElementById('myPaypalButtons').style.display = 'none';

                    }
                  });
                } else {
                  console.log('cancelled');
                  this.games.style.display = 'block';
                  // this.moneyToPay = 20;
                }
              }

            }
            // this.teller = 0;
            console.log(el.name, 'hahah join', this.loginservice.displayName);

          }
          else if (element.players.length - 1 === this.teller) {
            console.log(el.name, 'no u cant join', this.loginservice.displayName);
            // this.teller = 0;
            window.alert('Sorry you cannot play this game. Contact the Admin to join game');
          }
          this.teller = 0;
        });
        console.log('teller:', this.teller, 'players:', element.players.length);
      }

    });
    this.alertCounter = 0;
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
