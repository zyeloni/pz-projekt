import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './routes/home/home.component';
import { LoginComponent } from './routes/users/login/login.component';
import { RegisterComponent } from './routes/users/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TokenInterceptorService } from './interceptors/token.interceptor';
import { AddClubComponent } from './routes/clubs/add-club/add-club.component';
import { Select2Module } from 'ng-select2-component';
import { AuthService } from './services/auth.service';
import { JoinComponent } from './routes/clubs/join/join.component';
import { AddAidComponent } from './routes/aids/add-aid/add-aid.component';
import { ListAidsComponent } from './routes/aids/list-aids/list-aids.component';
import { CalendarAidsComponent } from './routes/aids/calendar-aids/calendar-aids.component';
import { FullCalendarModule } from '@fullcalendar/angular';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    AddClubComponent,
    JoinComponent,
    AddAidComponent,
    ListAidsComponent,
    CalendarAidsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    FlexLayoutModule,
    MatFormFieldModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    Select2Module,
    FullCalendarModule
  ],
  providers: [
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
