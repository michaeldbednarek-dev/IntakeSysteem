import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginModel } from '../models/LoginModel';
import { NgForm } from '@angular/forms';
import { AuthenticatedResponse } from '../models/LoginModel';
import { HttpErrorResponse } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

import { authService } from '../services/authService';


@Component({
  selector: 'app-inlog',
  templateUrl: './inlog.component.html',
  styleUrls: ['./inlog.component.scss']
})
export class InlogComponent implements OnInit{
  invalidLogin = false;
  credentials: LoginModel = {username:'', password:''};
  constructor(private router: Router, private http: HttpClient,private cookieService: CookieService,private userservice: authService) { }
  ngOnInit(): void {
  }
  login = ( form: NgForm) => {
    if (form.valid) {
     this.userservice.login(this.credentials).subscribe({
        next: (response: AuthenticatedResponse) => {
          const token = response.token;
          console.log(response);
          localStorage.setItem('jwt', token);
          this.invalidLogin = false;
          this.router.navigate(['/intake']);this.cookieService.set('user', JSON.stringify(this.credentials));
          console.log(this.cookieService.get('user'));
        },
        error: (err: HttpErrorResponse) => this.invalidLogin = true
      });
    }
  };
}
