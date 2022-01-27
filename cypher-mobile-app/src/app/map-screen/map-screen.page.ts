/* eslint-disable no-var */
/* eslint-disable prefer-arrow/prefer-arrow-functions */
/* eslint-disable guard-for-in */
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Geolocation } from '@capacitor/geolocation';
import { MenuController } from '@ionic/angular';
import { ApiService } from '../Services/api.service';

declare let google: any;

@Component({
  selector: 'app-map-screen',
  templateUrl: './map-screen.page.html',
  styleUrls: ['./map-screen.page.scss'],
})
export class MapScreenPage implements OnInit {

  map: any;
  coords: any;
  playerPosLat: number;
  playerPosLng: number;
  showPage: any;
  userMarker: any;
  playerWarningArea: any;
  playerStealArea: any;
  ghostHackerMarker: any;
  ghostHackerPos: any;
  stolen: boolean;
  level = 1;
  playerStoleItem = false;
  playerPoint = 0;
  ghostPoint = 0;
  //Antwerpen: Rooseveltplein
  // eslint-disable-next-line @typescript-eslint/no-inferrable-types
  ghostHackerPosLat: number = 51.219783;
  // eslint-disable-next-line @typescript-eslint/no-inferrable-types
  ghostHackerPosLng: number = 4.416215;
  //Coords for testing
  // ghostHackerPosLat: number = 51.3275;
  // ghostHackerPosLng: number = 4.9297;
  // ghostHackerPosLat: number = 51.2174;
  // ghostHackerPosLng: number = 3.7484;
  // eslint-disable-next-line @typescript-eslint/no-inferrable-types
  moveCoord: number = 0.0002;
  ghostHackerActivated = false;
  ghostHackerWarning = false;
  ghostHackerStoleSomething = false;
  inMapScreen = true;
  getPlayerInAnArea = false;
  randomNumber: any;
  inventoryItemToDelete: any;
  inventory: any;
  playerId: any;
  storeHackerActive: any;
  storeHackerPosLat: any;
  storeHackerPosLng: any;
  storeHackerSteal: any;
  getBoolActiveHacker: string;
  getBoolStealHacker: string;
  timeLeft = 600;
  interval;
  // eslint-disable-next-line @typescript-eslint/naming-convention
  ItemToTake: any;
  gameOver = false;
  kerk = false;
  askCounter = 0;
  // eslint-disable-next-line @typescript-eslint/naming-convention
  KerkLocation: any;
  // eslint-disable-next-line @typescript-eslint/naming-convention
  StreetLocation: any;



  // eslint-disable-next-line @typescript-eslint/member-ordering
  @ViewChild('map', { read: ElementRef, static: false }) mapRef: ElementRef;

  constructor(public router: Router, private menu: MenuController, public inventoryItem: ApiService) {
    this.inventoryItemToDelete = localStorage.getItem('inventoryItems');
    this.GetInventoryId();


  }

  ngOnInit() {
    this.inventoryItem.lobbyNaam = localStorage.getItem('LobbyName');
    if (this.inventoryItem.lobbyNaam === 'The Flash') {
      this.ghostHackerPosLat = 51.23080570155349;
      this.ghostHackerPosLng = 4.425015768864978;
      this.moveCoord = 0.00002;
    }


  }

  // eslint-disable-next-line @typescript-eslint/naming-convention
  GetInventoryId() {
    this.inventoryItem.getPlayerById(localStorage.getItem('playerId')).subscribe(i => {
      this.playerId = i;
      this.inventory = this.playerId.data.inventory.items;
    });
  }
  startTimer() {
    this.interval = setInterval(() => {
      if (this.timeLeft > 0) {
        this.timeLeft--;
      } else if (this.timeLeft === 0) {
        console.log('time is up ');
        this.gameOver = true;
        this.pauseTimer();
      }
      console.log(this.timeLeft);
      if (this.timeLeft === 3) {
        console.log('It is 3');
        console.log(location);
        // this.ItemToTake.setMap(null);
      }
    }, 1000);

  }

  pauseTimer() {
    clearInterval(this.interval);
  }

  async locate() {
    const coordinates = await Geolocation.getCurrentPosition();
    this.coords = coordinates.coords;
  }

  ionViewDidEnter() {
    this.getBoolActiveHacker = localStorage.getItem('hackerActive');
    if (this.getBoolActiveHacker != null) {
      this.ghostHackerActivated = (this.getBoolActiveHacker.toLowerCase() === 'true');
    } else {
      this.ghostHackerActivated = false;
    }

    this.getBoolStealHacker = localStorage.getItem('hackerSteal');
    if (this.getBoolStealHacker != null) {
      this.ghostHackerStoleSomething = (this.getBoolStealHacker.toLowerCase() === 'true');
    } else {
      this.ghostHackerStoleSomething = false;
    }

    this.showMap();
  }

  goToGamescreen() {
    this.router.navigate(['game-screen']).then(() => window.location.reload());
    this.inMapScreen = false;
  }
  // eslint-disable-next-line @typescript-eslint/naming-convention
  GoTo(page: string) {
    console.log('Called open ' + page);
    this.router.navigate([page]).then(() => window.location.reload());
    this.inMapScreen = false;
  }

  moveGhostHacker() {
    if (this.ghostHackerActivated && !this.ghostHackerStoleSomething) {
      if (this.ghostHackerPosLat > this.playerPosLat) {
        this.ghostHackerPosLat -= this.moveCoord;
      } else {
        this.ghostHackerPosLat += this.moveCoord;
      }

      if (this.ghostHackerPosLng > this.playerPosLng) {
        this.ghostHackerPosLng -= this.moveCoord;
      } else {
        this.ghostHackerPosLng += this.moveCoord;
      }
      var ghostHackerMarkerLatLng = new google.maps.LatLng(this.ghostHackerPosLat, this.ghostHackerPosLng);
      this.ghostHackerMarker.setPosition(ghostHackerMarkerLatLng);
    }
  }
  moveGhostToPlayer() {

    if (this.ghostHackerActivated) {
      if (this.ghostHackerPosLat > this.playerPosLat) {
        this.ghostHackerPosLat -= this.moveCoord;
      } else {
        this.ghostHackerPosLat += this.moveCoord;
      }

      if (this.ghostHackerPosLng > this.playerPosLng) {
        this.ghostHackerPosLng -= this.moveCoord;
      } else {
        this.ghostHackerPosLng += this.moveCoord;
      }
      var ghostHackerMarkerLatLng = new google.maps.LatLng(this.ghostHackerPosLat, this.ghostHackerPosLng);
      this.ghostHackerMarker.setPosition(ghostHackerMarkerLatLng);
    }
  }

  ghostToLocation(lat: any, lon: any) {
    // { lat: 51.242570, lng: 4.444350 }
    if (this.ghostHackerPosLat > lat) {
      // console.log('i am at the location');
      this.ghostHackerPosLat -= this.moveCoord;
    } else {
      this.ghostHackerPosLat += this.moveCoord;
    }

    if (this.ghostHackerPosLng > lon) {
      this.ghostHackerPosLng -= this.moveCoord;
    } else {
      this.ghostHackerPosLng += this.moveCoord;
    }
    var ghostHackerMarkerLatLng = new google.maps.LatLng(this.ghostHackerPosLat, this.ghostHackerPosLng);
    this.ghostHackerMarker.setPosition(ghostHackerMarkerLatLng);

  }


  async showMap() {
    if (!this.coords) {
      const coordinates = await Geolocation.getCurrentPosition();
      this.coords = coordinates.coords;
    }

    var mapStyle = [
      {
        featureType: 'poi',
        elementType: 'labels',
        stylers: [{ visibility: 'off' }]
      },
      {
        featureType: 'transit',
        elementType: 'labels',
        stylers: [{ visibility: 'off' }]
      }];

    const location = new google.maps.LatLng(this.coords.latitude, this.coords.longitude);
    const options = {
      center: location,
      zoom: 15,
      disableDefaultUI: true,
      styles: mapStyle
    };
    this.map = new google.maps.Map(this.mapRef.nativeElement, options);
    this.userMarker = new google.maps.Marker({
      icon: playerMarker,
      position: location,
      title: 'You are here!'
    });
    this.userMarker.setMap(this.map);
    const image = "https://img.icons8.com/material-outlined/24/000000/skull.png";
    this.ghostHackerMarker = new google.maps.Marker({
      position: this.ghostHackerPos,
      icon: image,
    })
    this.playerWarningArea = new google.maps.Circle({
      strokeColor: "#FF0000",
      strokeOpacity: 0,
      strokeWeight: 0,
      fillColor: "#FF0000",
      fillOpacity: 0,
      map: this.map,
      center: location,
      radius: 30,
    });
    this.playerStealArea = new google.maps.Circle({
      strokeColor: "#FF0000",
      strokeOpacity: 0,
      strokeWeight: 0,
      fillColor: "#FF0000",
      fillOpacity: 1,
      map: this.map,
      center: location,
      radius: 30,
    });


    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position: GeolocationPosition) => {
          var pos = {
            lat: position.coords.latitude,
            lng: position.coords.longitude,
          };
          this.playerPosLat = pos.lat;
          this.playerPosLng = pos.lng;

          this.map.setCenter(pos);
          this.userMarker.setPosition(pos);
          // eslint-disable-next-line @typescript-eslint/no-shadow
          navigator.geolocation.watchPosition(position => {
            pos = {
              lat: position.coords.latitude,
              lng: position.coords.longitude,
            };
            this.playerPosLat = pos.lat;
            this.playerPosLng = pos.lng;
            this.userMarker.setPosition(pos);
            this.playerWarningArea.setCenter(pos);
            this.playerStealArea.setCenter(pos);

            this.SearchRegionAreasForPlayer();
          });
        }
      );

      if (!this.ghostHackerActivated && !this.ghostHackerStoleSomething) {
        setTimeout(() => {
          this.ghostHackerActivated = true;
          this.ghostHackerMarker.setMap(this.map);
        }, 1000);
      } else if (!this.ghostHackerStoleSomething) {
        this.ghostHackerMarker.setMap(this.map);
      }

      setInterval(() => {
        if (this.gameOver) {
          // alert('Game over !');
          if (this.playerPoint > this.ghostPoint) {
            window.alert('Congratulations! you won');
          }
          else {
            window.alert('Sorry you got beaten by the GHOST');
            // window.confirm('Sorry you got beaten by the GHOST');
          }
          const confirm = window.confirm('Game over!  Play again ?');
          if (confirm) {
            window.location.reload();
            console.log('yes');
            this.gameOver = false;
          } else {
            this.gameOver = false;
            this.router.navigate(['lobbies']).then(() => window.location.reload());
          }

        }
        else {
          if (this.inventoryItem.lobbyNaam === 'The Flash') {
            // this.ghostHackerPosLat = 51.244740;
            // this.ghostHackerPosLng = 4.445240;
            if (!this.playerStoleItem) {
              if (this.level === 1) {
                // this.ghostToLocation(51.221814, 4.413618);
                this.moveCoord = 0.00002;
                this.ghostToLocation(51.230217, 4.416559);
                console.log('going to location 1');

              }
              else if (this.level === 2) {
                this.moveCoord = 0.00004;
                this.ghostToLocation(51.230522, 4.413747);
                console.log('going to location 2');

              }
              else if (this.level === 3) {
                this.moveCoord = 0.00006;
                this.ghostToLocation(51.229963, 4.414709);
                console.log('going to location 3');
              }
              else if (this.level === 4) {
                this.moveCoord = 0.00008;
                this.ghostToLocation(51.242910, 4.445740);
                console.log('going to location 4');
              };
            }
            else {
              this.moveGhostToPlayer();
            }
            this.CheckHackerInRangeOfPlayer();
            this.CheckHackerInRangeOfLocation();
          }
          else {
            this.moveGhostHacker();
            this.CheckHackerInRangeOfPlayer();
          }

        }
      }, 1000);





      if (this.inventoryItem.lobbyNaam === 'The Flash') {
        window.alert('Welcome to ' + this.inventoryItem.lobbyNaam + ' game. ' +
          'The Goal is to get all items before the Ghost does. All within 600 seconds(10 minutes)!');
        window.alert('Go to the Green circle  on the map to get the Item. The time starts NOW!!');
        this.ghostHackerMarker = new google.maps.Marker({
          position: this.ghostHackerPos,
          icon: image,
        });
        this.startTimer();
        this.ItemToTake = new google.maps.Circle({
          strokeColor: '#00FF00',
          strokeOpacity: 0.8,
          strokeWeight: 2,
          fillColor: '#00FF00',
          fillOpacity: 0.35,
          map: this.map,
          // center: { lat: 51.221814, lng: 4.413618 },
          // center: { lat: 51.242570, lng: 4.444350 },
          // // center: { lat: 51.242430, lng: 4.442860 },
          center: { lat: 51.230217, lng: 4.416559 },
          radius: 15,
        });

        if (!this.ghostHackerActivated) {
          this.ghostHackerPos = { lat: this.ghostHackerPosLat, lng: this.ghostHackerPosLng }
        } else {
          this.ghostHackerPosLat = Number(localStorage.getItem('hackerLat'));
          this.ghostHackerPosLng = Number(localStorage.getItem('hackerLng'));
          this.ghostHackerPos = { lat: this.ghostHackerPosLat, lng: this.ghostHackerPosLng }
        }



      }
      else if (this.inventoryItem.lobbyNaam === 'Tourist') {
        this.ghostHackerMarker.setMap(null);
        this.KerkLocation = new google.maps.Circle({
          strokeColor: '#00FF00',
          strokeOpacity: 0.8,
          strokeWeight: 2,
          fillColor: '#00FF00',
          fillOpacity: 0.35,
          map: this.map,
          center: { lat: 51.242908, lng: 4.445740 },
          radius: 70,
        });
        this.StreetLocation = new google.maps.Circle({
          strokeColor: '#00FF00',
          strokeOpacity: 0.8,
          strokeWeight: 2,
          fillColor: '#00FF00',
          fillOpacity: 0.35,
          map: this.map,
          // center: { lat: 51.242908, lng: 4.445740 },
          center: { lat: 51.242149, lng: 4.441660 },
          radius: 70,
        });
      }
      else {
        if (!this.ghostHackerActivated && !this.ghostHackerStoleSomething) {
          setTimeout(() => {
            this.ghostHackerActivated = true;
            this.ghostHackerMarker.setMap(this.map);
          }, 10000);
        } else if (!this.ghostHackerStoleSomething) {
          this.ghostHackerMarker.setMap(this.map);
        }
        for (const region in cityregions) {
          const regionCircle = new google.maps.Circle({
            strokeColor: cityregions[region].strokeColor,
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: cityregions[region].fillColor,
            fillOpacity: 0.35,
            map: this.map,
            center: cityregions[region].center,
            radius: cityregions[region].radius,
          });
        }
      }

    }



  }


  // eslint-disable-next-line @typescript-eslint/naming-convention
  SearchRegionAreasForPlayer() {
    // this.getPlayerInAnArea = false;
    // console.log(this.ItemToTake.center);

    if ((this.inventoryItem.lobbyNaam === 'The Flash')) {
      // eslint-disable-next-line max-len
      if (google.maps.geometry.spherical.computeDistanceBetween(this.userMarker.getPosition(), this.ItemToTake.center) <= this.ItemToTake.radius) {
        console.log('i am here');
        this.playerStoleItem = true;
        this.playerPoint += 1;
        console.log(this.ItemToTake.center.lat);
        this.levelChanger();
        this.playerStoleItem = false;
      }
    }
    else if (this.inventoryItem.lobbyNaam === 'Tourist') {
      if (google.maps.geometry.spherical.computeDistanceBetween(this.userMarker.getPosition(),
        this.KerkLocation.center) <= this.KerkLocation.radius) {
        console.log('you found me');
        this.askCounter++;
        if (this.askCounter === 1 || this.askCounter === 20) {
          const askUserToPlay = confirm('You are at the Kerk. Do you want to play the Tourist?');
          if (askUserToPlay) {
            localStorage.setItem('streets', 'Kerk');
            this.router.navigate(['tourist']).then(() => window.location.reload());
          }
        }
      }
      if (google.maps.geometry.spherical.computeDistanceBetween(this.userMarker.getPosition(),
        this.StreetLocation.center) <= this.StreetLocation.radius) {
        console.log('you found me');
        this.askCounter++;
        if (this.askCounter === 1 || this.askCounter === 20) {
          const askUserToPlay = confirm('You are at the Gemeentehuis Area. Do you want to play the Tourist?');
          if (askUserToPlay) {
            localStorage.setItem('streets', 'Gemeentehuis');
            this.router.navigate(['tourist']).then(() => window.location.reload());
          }
        }
      }

    }
    else {
      this.getPlayerInAnArea = false;
      for (const region in cityregions) {
        // eslint-disable-next-line max-len
        if (google.maps.geometry.spherical.computeDistanceBetween(this.userMarker.getPosition(), cityregions[region].center) <= cityregions[region].radius) {
          this.getPlayerInAnArea = cityregions[region].playerInArea;
          console.log(cityregions[region].playerInArea);
          if (cityregions[region].isGameStartable) {
            cityregions[region].isGameStartable = false;
            cityregions[region].playerInArea = true;
            cityregions[region].playGame();
          }
        }
      }
    }



  }

  level2() {
    // center: { lat: 51.229853, lng: 4.415807 },
    console.log('made');
    this.ItemToTake.setMap(null);
    this.ItemToTake = new google.maps.Circle({
      strokeColor: '#00FF00',
      strokeOpacity: 0.8,
      strokeWeight: 2,
      fillColor: '#00FF00',
      fillOpacity: 0.35,
      map: this.map,
      // // center: { lat: 51.247760, lng: 4.456310 },
      center: { lat: 51.230522, lng: 4.413747 },
      // center: { lat: 51.242570, lng: 4.444350 },
      radius: 15,
    });
  }
  level3() {
    // center: { lat: 51.229853, lng: 4.415807 },
    console.log('level 3');
    this.ItemToTake.setMap(null);
    this.ItemToTake = new google.maps.Circle({
      strokeColor: '#00FF00',
      strokeOpacity: 0.8,
      strokeWeight: 2,
      fillColor: '#00FF00',
      fillOpacity: 0.35,
      map: this.map,
      // center: { lat: 51.257780, lng: 4.452110 },
      // // center: { lat: 51.244740, lng: 4.445240 },
      center: { lat: 51.229963, lng: 4.414709 },
      radius: 15,
    });
  }
  level4() {
    console.log('level 4');
    this.ItemToTake.setMap(null);
    this.ItemToTake = new google.maps.Circle({
      strokeColor: '#00FF00',
      strokeOpacity: 0.8,
      strokeWeight: 2,
      fillColor: '#00FF00',
      fillOpacity: 0.35,
      map: this.map,
      // center: { lat: 51.257780, lng: 4.452110 },
      center: { lat: 51.242910, lng: 4.445740 },
      radius: 70,
    });
  }
  // eslint-disable-next-line @typescript-eslint/naming-convention
  CheckHackerInRangeOfPlayer() {
    if (this.inventoryItem.lobbyNaam !== 'Tourist') {

      if (google.maps.geometry.spherical.computeDistanceBetween(this.ghostHackerMarker.getPosition(),
        this.playerWarningArea.center) <= this.playerWarningArea.radius) {
        if (!this.ghostHackerWarning && !this.ghostHackerStoleSomething) {
          window.alert('WARNING: The hacker is very close! Be careful, he might sabotage you!');
          this.ghostHackerWarning = true;
        }
      } else {
        this.ghostHackerWarning = false;
      }
      if (google.maps.geometry.spherical.computeDistanceBetween(this.ghostHackerMarker.getPosition(),
        this.playerStealArea.center) <= this.playerStealArea.radius) {
        if (!this.ghostHackerStoleSomething) {
          this.randomNumber = localStorage.getItem('inventoryLength');
          this.inventoryItem.deleteInventoryItem(this.inventory[this.GenerateIdForHacker(0, (this.randomNumber - 1))].id).subscribe(d => {
            window.alert('The hacker stole one of your items!');
          });
          console.log('again');
          this.ghostHackerStoleSomething = true;
          this.playerStoleItem = false;
          console.log(this.level);
          if (this.playerPoint > 0) {
            this.playerPoint -= 1;
          }
          else {
            this.playerPoint = 0;
          }
          this.ghostPoint += 1;
          console.log('stolen');
          if (this.inventoryItem.lobbyNaam !== 'The Flash') {
            setInterval(() => {
              this.ghostHackerMarker.setMap(null);
            }, 5000);
          }

        }

      }
    }
  }
  levelChanger() {

    if (this.level === 1) {
      this.level = 2;
      console.log('level 2');
      this.level2();
    }

    else if (this.level === 2) {
      this.level = 3;
      this.level3();
    }
    else if (this.level === 3) {
      this.level = 4;
      this.level4();
    }
    else if (this.level === 4) {
      this.level = 5;
    }
    else if (this.level === 5) {
      this.gameOver = true;
      console.log('game over');
      this.level = 0;

    }

  }
  // eslint-disable-next-line @typescript-eslint/naming-convention
  CheckHackerInRangeOfLocation() {
    if (google.maps.geometry.spherical.computeDistanceBetween(this.ghostHackerMarker.getPosition(),
      this.ItemToTake.center) <= this.ItemToTake.radius) {
      if (this.playerStoleItem) {
        //Ghost goes and steal points from player
        this.moveGhostToPlayer();
      }
      else {
        //Ghost goes to location to take point
        console.log('i am at the location');

        this.levelChanger();
      }
      // else {
      //   this.level = 2;
      //   this.level2();
      // }

      this.ghostPoint += 1;
      // this.moveGhostHacker();
    }
  }


  // eslint-disable-next-line @typescript-eslint/naming-convention
  GenerateIdForHacker(min, max) {
    return (Math.floor(Math.random() * (max - min + 1) + min));
  }

  openNav() {

    document.getElementById('mySidenav').style.width = '250px';
    console.log('side nav opened');
  }

  closeNav() {
    document.getElementById('mySidenav').style.width = '0';
  }

  openCLI() {
    this.router.navigate(['cli']);
  }

  openWITS() {
    this.router.navigate(['worm-in-the-system']);
  }

  openGhostHacker() {
    alert('The Ghost Hacker is on your tale!');
  }

  openDecryption() {
    this.router.navigate(['decryption']);
  }

  renderPage(page: any) {
    if (page === 'inventory') {
      this.router.navigate(['inventory']
      );
    }
    this.showPage = page;

    this.closeNav();
  }

  ionViewWillLeave() {
    this.storeHackerActive = localStorage.setItem('hackerActive', JSON.stringify(this.ghostHackerActivated));
    this.storeHackerSteal = localStorage.setItem('hackerSteal', JSON.stringify(this.ghostHackerStoleSomething));
    this.storeHackerPosLat = localStorage.setItem('hackerLat', JSON.stringify(this.ghostHackerPosLat));
    this.storeHackerPosLng = localStorage.setItem('hackerLng', JSON.stringify(this.ghostHackerPosLng));
  }
}

interface CityRegion {
  center: google.maps.LatLngLiteral;
  radius: number;
  strokeColor: string;
  fillColor: string;
  isGameStartable?: boolean;
  playerInArea: boolean;

  playGame(): void;
}

const cityregions: Record<string, CityRegion> = {
  studentenbuurt: {
    center: { lat: 51.221814, lng: 4.413618 },
    radius: 250,
    strokeColor: '#FF0000',
    fillColor: '#FF0000',
    isGameStartable: true,
    playerInArea: false,
    // eslint-disable-next-line prefer-arrow/prefer-arrow-functions
    playGame() {
      console.log('Start Game 1');
    },
  },
  eilandje: {
    center: { lat: 51.233345, lng: 4.411115 },
    radius: 200,
    strokeColor: '#FFF300',
    fillColor: '#FFF300',
    isGameStartable: true,
    playerInArea: false,
    // eslint-disable-next-line prefer-arrow/prefer-arrow-functions
    playGame() {
      console.log('Start Game 2');
    },
  },
  meir: {
    center: { lat: 51.218094, lng: 4.408774 },
    radius: 220,
    strokeColor: '#27FF00',
    fillColor: '#27FF00',
    isGameStartable: true,
    playerInArea: false,
    playGame() {
      console.log('Start Game 3');
    },
  },
  test: {
    center: { lat: 51.216392, lng: 4.404195 },
    radius: 160,
    strokeColor: '#7E00FF',
    fillColor: '#7E00FF',
    isGameStartable: true,
    playerInArea: false,
    playGame() {
      console.log('Start Game 4');
    },
  },
  aphogeschool: {
    center: { lat: 51.229853, lng: 4.415807 },
    radius: 150,
    strokeColor: '#FF8000',
    fillColor: '#FF8000',
    isGameStartable: true,
    playerInArea: false,
    playGame() {
      console.log('Start Game 5');
    },
  },
  test2: {
    center: { lat: 51.231731, lng: 4.431389 },
    radius: 200,
    strokeColor: '#FF8000',
    fillColor: '#FF8000',
    isGameStartable: true,
    playerInArea: false,
    playGame() {
      console.log('Start Game 6');
    },
  },
  test3: {
    center: { lat: 51.229367, lng: 4.420595 },
    radius: 100,
    strokeColor: '#FF8000',
    fillColor: '#FF8000',
    isGameStartable: true,
    playerInArea: false,
    playGame() {
      console.log('Start Game 7');
    },
  },
};

const playerMarker = {
  fillColor: '#4285F4',
  fillOpacity: 1,
  path: google.maps.SymbolPath.CIRCLE,
  scale: 8,
  strokeColor: 'rgb(255,255,255)',
  strokeWeight: 2
};
