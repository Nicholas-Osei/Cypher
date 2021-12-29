/* eslint-disable no-var */
/* eslint-disable prefer-arrow/prefer-arrow-functions */
/* eslint-disable guard-for-in */
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Geolocation } from '@capacitor/geolocation';
import { markAsUntransferable } from 'worker_threads';
import { MenuController } from '@ionic/angular';
import { PlayerService } from '../Services/player.service';
import { getTranslationDeclStmts } from '@angular/compiler/src/render3/view/template';

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
  playerArea: any;
  ghostHackerMarker: any;
  ghostHackerPosition: any;
  // ghostHackerPosLat: number = 51.327253;
  // ghostHackerPosLng: number = 4.929911;
  // Back-up coords for when laptop is being a drama queen
  ghostHackerPosLat: number = 51.217299;
  ghostHackerPosLng: number = 3.747903;
  moveCoord: number = 0.0002;
  ghostHackerStoleSomething = false;
  inMapScreen = true;
  getPlayerInAnArea = false;
  randomNumber: any;
  inventoryItemToDelete: any;
  inventory: any;
  playerId: any;

  // eslint-disable-next-line @typescript-eslint/member-ordering
  @ViewChild('map', { read: ElementRef, static: false }) mapRef: ElementRef;

  constructor(public router: Router, private menu: MenuController, public inventoryItem: PlayerService) {
    this.inventoryItemToDelete = localStorage.getItem('inventoryItems');
    console.log(this.inventoryItemToDelete);
    this.GetInventoryId();
  }

  ngOnInit() {

  }



  async locate() {
    const coordinates = await Geolocation.getCurrentPosition();
    this.coords = coordinates.coords;
  }

  ionViewDidEnter() {
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

  moveGhostHacker(){
    if(this.ghostHackerPosLat > this.playerPosLat){
      this.ghostHackerPosLat -= this.moveCoord;
    } else {
      this.ghostHackerPosLat += this.moveCoord; 
    }

    if(this.ghostHackerPosLng > this.playerPosLng){
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
      position: { lat: this.ghostHackerPosLat, lng: this.ghostHackerPosLng },
      icon: image,
    })

    this.playerArea = new google.maps.Circle({
      strokeColor: "#FF0000",
      strokeOpacity: 0,
      strokeWeight: 0,
      fillColor: "#FF0000",
      fillOpacity: 0,
      map: this.map,
      center: location,
      radius: 20,
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
            this.playerArea.setCenter(pos);

            this.SearchRegionAreasForPlayer();
          });
        }
      );

      setTimeout(() => {
        this.ghostHackerMarker.setMap(this.map);
      }, 5000)

      setInterval(() => {
        this.moveGhostHacker();
        this.CheckHackerInRangeOfPlayer();
      }, 1000)
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

  // eslint-disable-next-line @typescript-eslint/naming-convention
  SearchRegionAreasForPlayer() {
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

  CheckHackerInRangeOfPlayer(){
    if (google.maps.geometry.spherical.computeDistanceBetween(this.ghostHackerMarker.getPosition(), this.playerArea.center) <= this.playerArea.radius){
      console.log("WARNING: The hacker is very close! Be careful, he might sabotage you!");
      if (!this.ghostHackerStoleSomething){
        this.randomNumber = localStorage.getItem('itemLength');
        this.inventoryItem.deleteInventoryItem(this.inventory[this.GenerateIdForHacker(0, (this.randomNumber - 1))].id).subscribe(d => {console.log("The hacker stole an item!", this.inventory[this.GenerateIdForHacker(0, (this.randomNumber - 1))].id);
        })
        this.ghostHackerStoleSomething = true;
      }
    }
  }

  GetInventoryId(){
    this.inventoryItem.getPlayerById(localStorage.getItem('playerId')).subscribe(i => {
      this.playerId = i;
      this.inventory = this.playerId.data.inventory.items;
      console.log(this.inventory);
      
    });
  }

  GenerateIdForHacker(min, max){
    return(Math.floor(Math.random() * (max - min + 1) + min));
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