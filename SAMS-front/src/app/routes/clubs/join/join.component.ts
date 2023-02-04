import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { ClubService } from 'src/app/services/club.service';

@Component({
  selector: 'app-join',
  templateUrl: './join.component.html',
  styleUrls: ['./join.component.css']
})
export class JoinComponent implements OnInit {

  clubsList: Array<any> = new Array<any>();

  constructor(private auth: AuthService, private clubs: ClubService, private router: Router) {
    this.clubs.GetClubs().subscribe((result: any) => {
      result.forEach((element: any) => {
        this.clubsList!.push({
          id: element.id,
          name: element.name,
          joined: this.joined(element.id)
        });
      });
    });
  }

  ngOnInit(): void {
    
  }

  joined(id: string){
    let res = this.auth.getUser().clubs.find((club: any) => club.scienceClub.id === id && club.active);
    return res!=null;
  }

  join(id: string){
    this.clubs.Join(id).subscribe(() => {
      var club = this.clubsList.find(x => x.id === id);
      club.joined = true;
      this.auth.refreshToken().subscribe();
    });
  }

  leave(id: string){
    this.clubs.Leave(id).subscribe(() => {
      let club = this.clubsList.find(x => x.id == id);
      club.joined = false;
      this.auth.refreshToken().subscribe();
    });
  }

  back(){
    this.router.navigate(['../']);
  }
}
