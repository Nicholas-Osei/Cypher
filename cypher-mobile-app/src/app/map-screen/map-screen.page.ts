import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Geolocation } from '@capacitor/geolocation';

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

  // eslint-disable-next-line @typescript-eslint/member-ordering
  @ViewChild('map', { read: ElementRef, static: false }) mapRef: ElementRef;

  constructor() { }

  ngOnInit() {
  }

  async locate() {
    const coordinates = await Geolocation.getCurrentPosition();
    this.coords = coordinates.coords;
  }

  ionViewDidEnter() {
    this.showMap();
  }

  async showMap() {
    if (!this.coords) {
      const coordinates = await Geolocation.getCurrentPosition();
      this.coords = coordinates.coords;
    }
    const location = new google.maps.LatLng(this.coords.latitude, this.coords.longitude);
    const options = {
      center: location,
      zoom: 15,
      disableDefaultUI: true
    };
    this.map = new google.maps.Map(this.mapRef.nativeElement, options);
    this.infoWindow = new google.maps.InfoWindow();

    const locationButton = document.createElement('button');

    locationButton.textContent = 'Pan to Current Location';
    locationButton.classList.add('custom-map-control-button');

    this.map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);

    locationButton.addEventListener('click', () => {
      // Try HTML5 geolocation.
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position: GeolocationPosition) => {
            const pos = {
              lat: position.coords.latitude,
              lng: position.coords.longitude,
            };

            this.infoWindow.setPosition(pos);
            this.infoWindow.setContent('Location found.');
            this.infoWindow.open(this.map);
            this.map.setCenter(pos);
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

    const regionCircle = new google.maps.Circle({
      strokeColor: "#FF0000",
      strokeOpacity: 0.8,
      strokeWeight: 2,
      fillColor: "#FF0000",
      fillOpacity: 0.35,
      map: this.map,
      center: { lat: 51.221814, lng: 4.413618 },
      radius: 250
    })

    const regionCircle2 = new google.maps.Circle({
      strokeColor: "#FFF300",
      strokeOpacity: 0.8,
      strokeWeight: 2,
      fillColor: "#FFF300",
      fillOpacity: 0.35,
      map: this.map,
      center: { lat: 51.233345, lng: 4.411115 },
      radius: 200
    })

    const regionCircle3 = new google.maps.Circle({
      strokeColor: "#27FF00",
      strokeOpacity: 0.8,
      strokeWeight: 2,
      fillColor: "#27FF00",
      fillOpacity: 0.35,
      map: this.map,
      center: { lat: 51.218094, lng: 4.408774 },
      radius: 220
    })
  }
}
