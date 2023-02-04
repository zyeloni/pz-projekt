import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ClubService } from 'src/app/services/club.service';

@Component({
  selector: 'app-add-club',
  templateUrl: './add-club.component.html',
  styleUrls: ['./add-club.component.css']
})
export class AddClubComponent implements OnInit {

  formGroup!: FormGroup;
  errors: String[] = [];
  commits: String[] = [];
  
  constructor(private clubService: ClubService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      name:new FormControl(null,[Validators.required]),
      join:new FormControl(null,[])
    })
  }

  addProcess(): void{
    this.errors = [];
    if(this.formGroup.valid){
      this.clubService.AddClub(this.formGroup.getRawValue()).subscribe((result: any) => {
        console.log(result);
        if(result instanceof HttpErrorResponse && result.status!=200){
          this.errors.push(result.message);
        }
        else if(result instanceof HttpErrorResponse && result.status==200){
          this.commits.push("The club was succesfully created");
        }
      });
    }else{
      if(this.formGroup.getError("required", "club")) this.errors.push("Club name is required");
    }
  }

  back(){
    this.router.navigate(['../']);
  }

}
