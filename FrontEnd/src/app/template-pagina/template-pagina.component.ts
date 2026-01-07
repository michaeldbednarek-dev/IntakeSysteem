import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../models/LoginModel';
import { templateService } from '../services/templateService';


@Component({
  selector: 'app-template-pagina',
  templateUrl: './template-pagina.component.html',
  styleUrls: ['./template-pagina.component.scss']
})
export class TemplatePaginaComponent implements OnInit {

  constructor(private templateService: templateService) { }

  ngOnInit(): void {
  }

  credentials: LoginModel = {username:'', password:''};

  public test()
  {
    this.templateService.test(this.credentials).subscribe();
  }

}
