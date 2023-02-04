import { apiPrintAids } from './../globals/url';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, mapTo, Observable, of, tap } from 'rxjs';
import { apiAddAid, apiGetAids, apiGetAllAids } from '../globals/url';

@Injectable({
  providedIn: 'root'
})
export class AidService {

  constructor(private http: HttpClient) { }

  addAid(aid: any): Observable<string>{
    return this.http.post(apiAddAid, aid).pipe(
      tap((result: any) => of(result)),
      catchError((error: any) => of(error))
    );
  }

  getAids(){
    return this.http.get(apiGetAids).pipe(
      tap((result: any) => of(result) ),
      catchError((error: any) => of(error))
    );
  }

  printAid(id: string){
    return this.http.get(`${apiPrintAids}?id=${id}`, {
      headers: {
        "Content-Type": "application/json",
        Accept: "application/pdf"
      },
      responseType: "blob"
    }).pipe(
      tap((result: any) => of(result) ),
      catchError((error: any) => of(error))
    );
  }

  getAllAids(){
    return this.http.get(apiGetAllAids).pipe(
      tap((result: any) => of(result) ),
      catchError((error: any) => of(error))
    );
  }
}
