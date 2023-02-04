import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ClubService } from 'src/app/services/club.service';
import { Select2Option } from 'ng-select2-component';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  formGroup: FormGroup = new FormGroup({
    name:new FormControl(null,[Validators.required]),
    surname:new FormControl(null,[Validators.required]),
    email:new FormControl(null,[Validators.required,Validators.email]),
    password:new FormControl(null,[Validators.required]),
    password2:new FormControl(null,[Validators.required]),
    clubs: new FormControl(null, [Validators.required])
  });
  errors: String[] = [];
  commits: String[] = [];
  clubsList!: Array<Select2Option>;

  constructor(private authService: AuthService, private clubsService: ClubService) { 
    var c: Array<Select2Option> = [];
    this.clubsService.GetClubs().subscribe((result: any) => {
      result.forEach((element: any) => {
        c.push({ id: element.id, value: element.value, label: element.name });
      });
      this.clubsList = c;
    });
  }

  ngOnInit(): void {
    
  }

  registerProcess(){
    this.errors = [];
    if(this.formGroup.valid){
      if(this.formGroup.get('password')?.value!==this.formGroup.get('password2')?.value){
        this.errors.push("You must confirm your password");
      }else{
        this.authService.registerUser(this.formGroup.getRawValue()).subscribe((result: any) => {
          if(result instanceof HttpErrorResponse && result.status!=200){
            this.errors.push(result.message);
          }
          else if(result=true){
            this.commits.push("You were succesfully registered");
          }
        });
      }
    }else{
      if(this.formGroup.getError("required", "name")) this.errors.push("Name is required");
      if(this.formGroup.getError("required", "surname")) this.errors.push("Surname is required");
      if(this.formGroup.getError("required", "email")) this.errors.push("Email is required");
      if(this.formGroup.getError("email", "email")) this.errors.push("Email: mail@mail.com");
      if(this.formGroup.getError("required", "password")) this.errors.push("Password is required");
      if(this.formGroup.getError("required", "password2")) this.errors.push("You must confirm your password");
      if(this.formGroup.getError("required", "clubs")) this.errors.push("You must register to your club(s)");
    }
  }

}
