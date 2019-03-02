import { NursingStationComponent } from './nursing-station/nursing-station.component';
import { PreparationListComponent } from './preparation-list/preparation-list.component';
import { DistributionScheduleComponent } from './distribution-schedule/distribution-schedule.component';
import { NursesComponent } from './nurses/nurses.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './login/login.component';
import { DoctorComponent } from './doctor/doctor.component';
import { PharmacistComponent } from './pharmacist/pharmacist.component';
import { StaffComponent } from './staff/staff.component';
import { AdminComponent } from './admin/admin.component';

const routes: Routes = [
  { path: 'admin', component: AdminComponent },
  { path: 'user', component: UserComponent },
  { path: 'login', component: LoginComponent },
  { path: 'doctor', component: DoctorComponent },
  { path: 'nurses', component: NursesComponent },
  {
    path: 'pharmacist',
    component: PharmacistComponent
  },
  { path: 'pharmacist/distribution-schedule', component: DistributionScheduleComponent },
  { path: 'pharmacist/preparation-list', component: PreparationListComponent },
  { path: 'pharmacist/nursing-station', component: NursingStationComponent },

  {
    path: 'nurses',
    component: NursesComponent
  },

  { path: 'nurses/nursing-station', component: NursingStationComponent },

  { path: 'staff', component: StaffComponent },
  { path: '', component: LoginComponent }
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
  declarations: []
})
export class AppRoutingModule { }