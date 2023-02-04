import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AidService } from 'src/app/services/aid.service';
import { AuthService } from 'src/app/services/auth.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-list-aids',
  templateUrl: './list-aids.component.html',
  styleUrls: ['./list-aids.component.css']
})
export class ListAidsComponent implements OnInit {

  aidsList!: Array<any>;
  checkAdmin = false;

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
      
    });
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