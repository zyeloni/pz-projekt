import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  authenticated: boolean = false;
  user: any;

  constructor(private auth: AuthService, private router: Router) {
    
  }

  ngOnInit(): void {
    this.authenticated = this.auth.isUserLogged();
    this.auth.getToken().subscribe(res => {
      if(res) this.user = res.user
    });
  }

  getName(){
    return this.user?.name;
  }

  isHeadAdmin(){
    return this.user?.role=="Admin";
  }

  routeAddClub(){
    this.router.navigate(['/clubs/add']);
  }

  routeAddAid(){
    this.router.navigate(['/aids/add']);
  }

  routeListAid(){
    this.router.navigate(['/aids']);
  }

  routeJoin(){
    this.router.navigate(['/clubs/join']);
  }
  routeCalendar() {
    this.router.navigate(['/aids/calendar']);
  }

  logout(){
    this.auth.logout();
    window.location.reload();
  }

}
