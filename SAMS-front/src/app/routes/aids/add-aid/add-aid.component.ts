import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Select2Option } from 'ng-select2-component';
import { AidService } from 'src/app/services/aid.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-add-aid',
  templateUrl: './add-aid.component.html',
  styleUrls: ['./add-aid.component.css']
})
export class AddAidComponent implements OnInit {

  clubsList!: Array<Select2Option>;

  formGroup: FormGroup = new FormGroup({
    scienceClubID: new FormControl(null, [Validators.required]),
    subject: new FormControl(null, [Validators.required]),
    comment: new FormControl(null, [Validators.required]),
    count: new FormControl(1, [Validators.required]),
    dateTime: new FormControl(null, [Validators.required]),
  });

  constructor(private aid: AidService, private auth: AuthService, private router: Router) {}

  ngOnInit(): void {
    var c: Array<Select2Option> = [];
    this.auth.getUser().clubs.forEach((element: any) => {
      let club = element.scienceClub;
      c.push({
        id: club.id, 
        value: club.id,
        label: club.name
      });
    });
    this.clubsList = c;
  }

  back(){
    this.router.navigate(['../']);
  }

  addAidProcess(){
    this.aid.addAid(this.formGroup.getRawValue()).subscribe((result: any) =>{
      console.log(result);
      this.router.navigate(['../aids']);
    });
  }

}
