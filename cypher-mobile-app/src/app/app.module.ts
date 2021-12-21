import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { GooglePlus } from '@ionic-native/google-plus/ngx';
import { HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';


@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  // eslint-disable-next-line max-len
  imports: [BrowserModule, IonicModule.forRoot(), AppRoutingModule, HttpClientModule, FontAwesomeModule, SocialLoginModule, ServiceWorkerModule.register('ngsw-worker.js', {
    enabled: environment.production,
    // Register the ServiceWorker as soon as the app is stable
    // or after 30 seconds (whichever comes first).
    registrationStrategy: 'registerWhenStable:30000'
  })],
  providers: [
    GooglePlus,
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '646712186754-c36mk5hmluv34o2lk8vmgk2h6p3ne1pk.apps.googleusercontent.com'
            )
          }
        ]
      } as SocialAuthServiceConfig
    },
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent],
})
export class AppModule { }
