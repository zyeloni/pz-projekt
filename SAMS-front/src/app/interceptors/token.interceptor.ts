import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, filter, Observable, switchMap, take, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
 
@Injectable()
export class TokenInterceptorService implements HttpInterceptor {
    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(private auth: AuthService){}

    intercept(req: HttpRequest<any>, next: HttpHandler):Observable<HttpEvent<any>> {
        var request = req;
        this.auth.getToken().subscribe(val => {
            request = this.addToken(req, val.token);
        });

        return next.handle(request).pipe(catchError(error => {
            if(error instanceof HttpErrorResponse && error.status==401){
                return this.handle401(request, next);
            }else{
                return throwError(error);
            }
        }));
    }

    addToken(req: HttpRequest<any>, token: any){
        return req.clone({
            setHeaders: {
                'Authorization': `Bearer ${token}`
            }
        });
    }

    handle401(req: HttpRequest<any>, next: HttpHandler){
        if(!this.isRefreshing){
            return this.refreshToken(req, next);
        }else{
            return this.refreshTokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(jwt => {
                    return next.handle(this.addToken(req, jwt.value.Token));
                })
            );
        }
    }
    refreshToken(req: HttpRequest<any>, next: HttpHandler) {
        this.isRefreshing = true;
        this.refreshTokenSubject.next(null);
        return this.auth.refreshToken().pipe(
            switchMap((token:any) => {
                this.isRefreshing = false;
                this.refreshTokenSubject.next(token.value.token);
                return next.handle(this.addToken(req, token.value.token))
            })
        );
    }
}