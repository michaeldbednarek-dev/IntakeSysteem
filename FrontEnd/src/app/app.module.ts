import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IntakePlannenComponent } from './intake-plannen/intake-plannen.component';
import { TemplatePaginaComponent } from './template-pagina/template-pagina.component';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InlogComponent } from './inlog/inlog.component';

export function tokenGetter(){
  return localStorage.getItem('jwt');
}
@NgModule({
  declarations: [
    AppComponent,
    InlogComponent,
    IntakePlannenComponent,
    TemplatePaginaComponent,
    InlogComponent
  ],
  imports: [
    RouterModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:4200'],
        disallowedRoutes: []
      }
    }),
    CommonModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,


  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
