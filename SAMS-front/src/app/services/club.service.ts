import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, mapTo, Observable, of, tap } from 'rxjs';
import { apiAddClub, apiGetClubs, apiLeaveClub, apiJoinClub } from '../globals/url';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ClubService {
  

  constructor(private auth: AuthService, private http: HttpClient) { }

  AddClub(club: any): Observable<string>{
    return this.http.post(apiAddClub, club).pipe(
      tap((result: any) => {
        console.log(result);
        return of(result);
      }),
      mapTo("ok"),
      catchError((error: any) => {
        return of(error);
      })
    );
  }

  GetClubs(){
    return this.http.get(apiGetClubs);
  }

  Leave(id: string) {
    return this.http.get(apiLeaveClub+ id).pipe(
      tap((result: any) => {
        console.log(result);
        return of(result);
      }),
      mapTo("ok"),
      catchError((error: any) => {
        return of(error);
      })
    );
  }

  Join(id: string) {
    return this.http.get(apiJoinClub + id).pipe(
      tap((result: any) => {
        console.log(result);
        return of(result);
      }),
      mapTo("ok"),
      catchError((error: any) => {
        return of(error);
      })
    );
  }

}
