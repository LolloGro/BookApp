import { Component, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {AppHeader} from './components/app-header/app-header';

@Component({
  selector: 'app-root',
  imports: [AppHeader, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('BookFrontend');
}
