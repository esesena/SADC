import { User } from './models/identity/User';
import { AccountService } from './services/account.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'SADC';
  isAuthenticated = false;

  constructor(public accountService: AccountService){}

  ngOninit(): void {
    if (this.accountService.currentUser$ !== null) {
      this.isAuthenticated = true;
    }

    this.setCurrentUser();
  }

  setCurrentUser(): void {
    let user: User;

    if (localStorage.getItem('user'))
      user = JSON.parse(localStorage.getItem('user') ?? '{}')

    else
      user = null

    if (user)
      this.accountService.setCurrentUser(user);
  }
}
