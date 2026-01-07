import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AppModule } from './app.module';
import { InlogComponent } from './inlog/inlog.component';
import { IntakePlannenComponent } from './intake-plannen/intake-plannen.component';
import { TemplatePaginaComponent } from './template-pagina/template-pagina.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: 'intake',
    component: IntakePlannenComponent, canActivate: [AuthGuard]
  },
  {
    path: 'template',
    component: TemplatePaginaComponent, canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: InlogComponent
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
