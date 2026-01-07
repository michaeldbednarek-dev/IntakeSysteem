import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AuthenticatedResponse } from '../models/LoginModel';

@Injectable({
  providedIn: 'root'
})
export class authService{

  constructor(private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44319/api/Auth/';
  readonly loginURI = this.BaseURI + 'login';
  readonly newEventURI = 'CalendarEvent';

  login(loginForm:any)
  {
    return this.http.post<any>(this.loginURI,loginForm, {
      headers: new HttpHeaders({ 'content-type': 'application/json'})
    });
  }
}
