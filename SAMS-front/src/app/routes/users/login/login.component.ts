import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpClientModule, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Access-Control-Allow-Origin':'*'
  })
};

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formGroup!: FormGroup;
  errors: String[] = [];
  active: boolean = true;

  constructor(private formBuilder: FormBuilder, private auth: AuthService, private router: Router) {
    
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      email:new FormControl(null,[Validators.required,Validators.email]),
      password:new FormControl(null,[Validators.required])
    })
  }

  loginProcess(){
    this.errors = [];
    if(this.formGroup.valid){
      this.active = false;
      this.auth.login(this.formGroup.getRawValue()).subscribe({
        next: (res: any) => { 
          if(res instanceof HttpErrorResponse) this.errors.push(res.error);
          else this.router.navigate(['../']);
        },
        error: (error: HttpErrorResponse) => this.errors.push(error.error.value)
      });
      this.active = true;
    }else{
      if(this.formGroup.getError("required", "email")) this.errors.push("Email is required");
      if(this.formGroup.getError("email", "email")) this.errors.push("Email: mail@mail.com");
      if(this.formGroup.getError("required", "password")) this.errors.push("Password is required");
    }
  }
}
