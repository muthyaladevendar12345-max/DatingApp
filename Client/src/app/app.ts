import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";
import { AccountService } from '../core/services/account-service';
import { Home } from "../features/home/home";
import { User } from '../types/user';


@Component({
  selector: 'app-root',
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  private httpClient = inject(HttpClient);
  protected readonly title = "Dating App";
  protected members = signal<User[]>([]);

  async ngOnInit() {

    // this.httpClient.get("https://localhost:5001/api/members").subscribe({
    //   next: response => this.members.set(response),
    //   error: error => console.log(error),
    //   complete: () => console.log("Request completed")
    // });
    this.members.set(await this.Getmembers());
      this.setCurrentUser();
  }
  setCurrentUser() {
const userString = localStorage.getItem('user');
  if (!userString) return;
  const user = JSON.parse(userString);
  this.accountService.currentUser.set(user);
  }


  async Getmembers() {
    try {
      return lastValueFrom(this.httpClient.get<User[]>("https://localhost:5001/api/members"));
    } catch (error) {
      console.log(error);
      return [];
    }
  }
}
