import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {  provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './services/auth.interceptor';
import { ModalModule } from 'ngx-bootstrap/modal';


export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),provideHttpClient(), provideAnimationsAsync(),
    importProvidersFrom(ModalModule.forRoot()),
    provideHttpClient(withInterceptors([authInterceptor]))]
};
