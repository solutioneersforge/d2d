import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-alert',
  imports: [],
  templateUrl: './alert.component.html',
  styleUrl: './alert.component.css'
})
export class AlertComponent {
 @Input() type: string = '';
  @Input() message: string = '';
  @Input() dismissible: boolean = true;
  @Input() timeout: number = 5000; // in ms

  @Output() closed = new EventEmitter<void>();

  ngOnInit() {
    if (this.timeout > 0) {
      setTimeout(() => this.close(), this.timeout);
    }
  }

  close() {
    this.closed.emit();
  }
}
