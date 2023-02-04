import { CalendarAidsComponent } from './routes/aids/calendar-aids/calendar-aids.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from './guards/authentication.guard';
import { RoleGuard } from './guards/role.guard';
import { AddAidComponent } from './routes/aids/add-aid/add-aid.component';
import { ListAidsComponent } from './routes/aids/list-aids/list-aids.component';
import { AddClubComponent } from './routes/clubs/add-club/add-club.component';
import { JoinComponent } from './routes/clubs/join/join.component';
import { HomeComponent } from './routes/home/home.component';
import { LoginComponent } from './routes/users/login/login.component';
import { RegisterComponent } from './routes/users/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'users/login', component: LoginComponent },
  { path: 'users/register', component: RegisterComponent },
  { path: 'clubs/add', component: AddClubComponent, canActivate: [AuthenticationGuard, RoleGuard], data: { role: "Admin" } },
  { path: 'clubs/join', component: JoinComponent, canActivate: [AuthenticationGuard]},
  { path: 'aids/add', component: AddAidComponent, canActivate: [AuthenticationGuard]},
  { path: 'aids', component: ListAidsComponent, canActivate: [AuthenticationGuard]},
  { path: 'aids/calendar', component: CalendarAidsComponent, canActivate: [AuthenticationGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
