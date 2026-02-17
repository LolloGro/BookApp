import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable, tap} from 'rxjs';

export interface Login {token: string}

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  private api = 'http://localhost:5249/api/Auth';
  private tokenKey = "token";
  private isLoggedIn():boolean{
    return !!this.getToken();
  }
  private loggedIn = new BehaviorSubject<boolean>(this.isLoggedIn());
  loggedIn$: Observable<boolean> = this.loggedIn.asObservable();
  constructor(private http: HttpClient) { }

  loginUser(username: string, password: string):Observable<Login> {
    return this.http.post<Login>(`${this.api}/login`,{
      username,
      password
    }).pipe(tap(res => {
      this.saveToken(res.token)
      this.loggedIn.next(true);
    }));
  }

  saveToken(token: string){
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(){
    return localStorage.getItem(this.tokenKey);
  }

  logout(){
    localStorage.removeItem(this.tokenKey);
    this.loggedIn.next(false);
  }

}
