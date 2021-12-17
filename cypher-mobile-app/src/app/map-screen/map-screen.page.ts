/* eslint-disable guard-for-in */
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Geolocation } from '@capacitor/geolocation';
import { markAsUntransferable } from 'worker_threads';

declare let google: any;

@Component({
  selector: 'app-map-screen',
  templateUrl: './map-screen.page.html',
  styleUrls: ['./map-screen.page.scss'],
})
export class MapScreenPage implements OnInit {

  map: any;
  coords: any;
  infoWindow: any;
  showPage: any;
  userMarker: any;
  inMapScreen = true;

  // eslint-disable-next-line @typescript-eslint/member-ordering
  @ViewChild('map', { read: ElementRef, static: false }) mapRef: ElementRef;

  constructor(public router: Router) {

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
  async showMap() {
    if (!this.coords) {
      const coordinates = await Geolocation.getCurrentPosition();
      this.coords = coordinates.coords;
    }

    var mapStyle = [
    {
      featureType: "poi",
      elementType: "labels",
      stylers: [{ visibility: "off" }]
    },
    {  
      featureType: "transit",
      elementType: "labels",
      stylers: [{ visibility: "off" }]
    }];

    const location = new google.maps.LatLng(this.coords.latitude, this.coords.longitude);
    const options = {
      center: location,
      zoom: 15,
      disableDefaultUI: true,
      styles: mapStyle
    };
    this.map = new google.maps.Map(this.mapRef.nativeElement, options);
    this.infoWindow = new google.maps.InfoWindow();
    this.userMarker = new google.maps.Marker({
      icon: playerMarker,
      position: location,
      title: 'You are here!'
    });
    this.userMarker.setMap(this.map);

    const locationButton = document.createElement('button');

    locationButton.textContent = 'Pan to Current Location';
    locationButton.classList.add('custom-map-control-button');

    this.map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);

    locationButton.addEventListener('click', () => {
      // Try HTML5 geolocation.
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position: GeolocationPosition) => {
            var pos = {
              lat: position.coords.latitude,
              lng: position.coords.longitude,
            };

            this.infoWindow.setPosition(pos);
            this.infoWindow.setContent('Location found.');
            this.infoWindow.open(this.map);
            this.map.setCenter(pos);
            this.userMarker.setMap(this.map);
            navigator.geolocation.watchPosition(position => {
              pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude,
              };
              this.userMarker.setMap(this.map);
            })
          },
          () => {
            // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
            handleLocationError(true, this.infoWindow, this.map.getCenter()!);
          }
        );
      } else {
        // Browser doesn't support Geolocation
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        handleLocationError(false, this.infoWindow, this.map.getCenter()!);
      }
      function handleLocationError(
        browserHasGeolocation: boolean,
        infoWindow: google.maps.InfoWindow,
        pos: google.maps.LatLng
      ) {
        infoWindow.setPosition(pos);
        infoWindow.setContent(
          browserHasGeolocation
            ? 'Error: The Geolocation service failed.'
            : 'Error: Your browser doesn\'t support geolocation.'
        );
        infoWindow.open(this.map);
      }
    });

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


  openNav() {

    document.getElementById('mySidenav').style.width = '250px';
    console.log('hmmm');
  }

  closeNav() {
    document.getElementById('mySidenav').style.width = '0';
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
}

const cityregions: Record<string, CityRegion> = {
  studentenbuurt: {
    center: { lat: 51.221814, lng: 4.413618 },
    radius: 250,
    strokeColor: '#FF0000',
    fillColor: '#FF0000',
  },
  eilandje: {
    center: { lat: 51.233345, lng: 4.411115 },
    radius: 200,
    strokeColor: '#FFF300',
    fillColor: '#FFF300',
  },
  meir: {
    center: { lat: 51.218094, lng: 4.408774 },
    radius: 220,
    strokeColor: '#27FF00',
    fillColor: '#27FF00',
  }
};

const playerMarker = {
  fillColor: '#4285F4',
  fillOpacity: 1,
  path: google.maps.SymbolPath.CIRCLE,
  scale: 8,
  strokeColor: 'rgb(255,255,255)',
  strokeWeight: 2
}