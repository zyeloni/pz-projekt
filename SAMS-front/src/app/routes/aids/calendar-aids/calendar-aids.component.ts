import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CalendarOptions, EventInput, EventSourceInput } from '@fullcalendar/core';
import daygrid from '@fullcalendar/daygrid';
import * as saveAs from 'file-saver';
import { AidService } from 'src/app/services/aid.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-calendar-aids',
  templateUrl: './calendar-aids.component.html',
  styleUrls: ['./calendar-aids.component.css']
})
export class CalendarAidsComponent implements OnInit {

  aidsList!: Array<any>;
  checkAdmin = false;
  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    events: [
      { title: 'event 1', date: '2021-03-01' },
    ],
    plugins: [daygrid],
    // time format 24h
    eventTimeFormat: {
      hour: '2-digit',
      minute: '2-digit',
      hourCycle: 'h23'
    },

  }

  constructor(private aids: AidService, private http: HttpClient, private router: Router, private auth:AuthService) { }

  ngOnInit(): void {
    this.getNonAdmin();
  }

  getNonAdmin(){
    this.checkAdmin = false;
    this.aids.getAids().subscribe((result: any) => {
      this.initializeList(result);
    });
  }

  initializeList(result: any){
    this.aidsList = [];
    let events: EventInput[] = [];
    

    result.forEach((element: any) => {
      this.aidsList!.push({
        id: element.id,
        name: element.user.name,
        surname: element.user.surname,
        time: element.dateTime.replace('T', ' '),
        subject: element.subject,
        comment: element.comment,
        count: element.count,
        club: element.scienceClub.name
      });
      
      events.push({
         title: `${element.scienceClub.name} - ${element.subject}`,
         start: element.dateTime,
        });
    });

    this.calendarOptions.events = events;
  }

  isHeadAdmin(){
    return this.auth.getUser().role=="Admin";
  }

  getAdmin(){
    this.checkAdmin = true;
    this.aidsList = [];
    this.aids.getAids().subscribe((result: any) => {
      this.initializeList(result);
    });
  }

  back(){
    this.router.navigate(['../']);
  }

  routeAdd(){
    this.router.navigate(['aids/add']);
  }

  print(id: string){
    
    this.aids.printAid(id).subscribe((result: any) => {
      saveAs(result, "report.pdf")
    });
  }

}
