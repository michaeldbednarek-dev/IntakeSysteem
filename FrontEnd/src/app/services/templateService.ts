import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AuthenticatedResponse } from '../models/LoginModel';

@Injectable({
  providedIn: 'root'
})
export class templateService{

  constructor(private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44319/api/Template/';
  readonly testURI = this.BaseURI + 'MakeThisVerySpecific';

  test(testthingin:any)
  {
    return this.http.post<any>(this.testURI,testthingin, {
      headers: new HttpHeaders({ 'content-type': 'application/json'})
    });
  }
}
