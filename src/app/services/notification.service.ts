import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private showNotificationSource = new Subject<string>();
  showNotification$ = this.showNotificationSource.asObservable();

  show(message: string) {
    this.showNotificationSource.next(message);
  }
}
