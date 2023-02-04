import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, mapTo, Observable, of, pipe, tap } from 'rxjs';
import { apiLogin, apiRefresh, apiRegister } from '../globals/url';

@Injectable({ providedIn: 'root' })
export class AuthService{
  
  private subject = new BehaviorSubject<any>(null);

  token: Observable<any> = this.subject.asObservable();

  constructor(private http: HttpClient) { }

  ngOnInit(): void {

  }

  getToken(){
    return this.token.pipe(map((result: any) => result));
  }

  login(user: {email: string, password: string}): Observable<any>{
    return this.http.post(apiLogin, user).pipe(
      map((result: any) => {
        this.subject.next(result);
        return result;
      }),
      catchError((error:HttpErrorResponse) => {
        return of(error);
      })
    );
  }

  isUserLogged(){
    return this.subject.value != null;
  }

  registerUser(user: any) {
    return this.http.post(apiRegister, user).pipe(
      map(() => {
        return true;
      }),
      catchError(() => {
        return of(false);
      })
    );
  }

  getUser(){
    return this.subject.value?.user;
  }

  logout(){
    this.subject.next(null);
  }

  getTokenResponse(){
    return this.subject.getValue();
  }

  refreshToken(){
    return this.http.post(apiRefresh, this.getTokenResponse()).pipe(
      map((result: any) => {
        this.subject.next(result);
        return result;
      }),
      catchError((error:HttpErrorResponse) => {
        console.log(error);
        return of(error.error.value);
      })
    );
  }
}
