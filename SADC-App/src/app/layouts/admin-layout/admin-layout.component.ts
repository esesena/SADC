import { User } from './../../models/identity/User';
import { AccountService } from './../../services/account.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  constructor(public accountService: AccountService) { }

  ngOnInit() {
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
