import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

export interface RegisterUser {
  username: string;
  password: string;
  email: string;
}

@Injectable({
  providedIn: 'root'
})

export class RegisterService {

  private api = '/api/Auth';
  constructor(private http: HttpClient) { }

  register(data:RegisterUser){
    return this.http.post<any>(`${this.api}/register`, data);
  }
}
