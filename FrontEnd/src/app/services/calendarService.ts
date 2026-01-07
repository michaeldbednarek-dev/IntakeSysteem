/* eslint-disable no-var */
/* eslint-disable @typescript-eslint/member-ordering */
/* eslint-disable @typescript-eslint/naming-convention */
import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ConnectConfig } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class calendarService{

  constructor(private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44319/api/Intake/';
  readonly newEventURI = this.BaseURI + 'CalendarEvent';

  public newCalendarEvent(intakeForm: any)
  {
    return this.http.post<any>(this.newEventURI,intakeForm, {
      headers: new HttpHeaders({ 'content-type': 'application/json'})
    });
  }
}
