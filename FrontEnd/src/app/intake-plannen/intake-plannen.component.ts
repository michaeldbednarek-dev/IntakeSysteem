import { Component, OnInit } from '@angular/core';
import { calendarService } from '../services/calendarService';
import { CalendarEvent } from '../models/CalendarEvent';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-intake-plannen',
  templateUrl: './intake-plannen.component.html',
  styleUrls: ['./intake-plannen.component.scss']
})
export class IntakePlannenComponent implements OnInit {


  intake: CalendarEvent = {Name:'',Date: '',startTime:'',CC:'',Color:0,location:'',Token:"ERROR"};

  constructor(private calendarS: calendarService, private toastr: ToastrService){}
  ngOnInit(): void
  {

  }

  public newIntake = (form: NgForm) =>
  {
    console.log(this.intake);
    this.intake.Date = this.intake.Date.toString();
    this.intake.startTime = this.intake.startTime.toString();
    this.intake.Token = localStorage.getItem('jwt')?.toString();

    this.intake.Color = Number(this.intake.Color);



    if (form.valid) {
      this.toastr.show('De intake was verstuurd', 'Verstuurd  ');
      var Er = '';
      this.calendarS.newCalendarEvent(this.intake).subscribe({
        error:(err: HttpErrorResponse) => {
          Er = err.toString();
          if (err.status == 400)
            this.toastr.error('Iets is niet goed ingevoeld.', 'Error!');
          else if (err.status == 500)
            this.toastr.error('Iets gaat mis met de server.', 'Error!');
          else if (err.status == 404)
            this.toastr.error('Server staat uit.', 'Error!');
          else
            this.toastr.error('Server accepteerd de aanvraag niet.', 'Error!');
        },
        next: (response: any) =>{
          if(Er == '')
          {
            this.toastr.success('De intake afspraak is geplanned!', 'Gelukt!');
          }
        }
      },


      );


    }
  }


}
