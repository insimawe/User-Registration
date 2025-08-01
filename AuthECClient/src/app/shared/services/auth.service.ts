import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http:HttpClient) { }
  baseURL = 'http://localhost:5274/api';

  createUser(formData:any){
    return this.http.post(this.baseURL+'/signup',formData);
  }

}
