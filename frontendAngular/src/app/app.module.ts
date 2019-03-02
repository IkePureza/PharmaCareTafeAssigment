import 'hammerjs';
import { PrescriptionsService } from './services/prescriptions.service';
import { LoginService } from './services/login.service';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { CustomMaterialModule } from './ng-material/material.module';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { DoctorComponent } from './doctor/doctor.component';
import { HttpModule } from "@angular/http";
import { PharmacistComponent, DialogContentExampleDialog } from './pharmacist/pharmacist.component';
import { NursesComponent, NursesDialogue } from './nurses/nurses.component';
import { StaffComponent } from './staff/staff.component';
import { CardComponent } from './card/card.component';
import { ExpandedCardComponent } from './expanded-card/expanded-card.component';
import { AdminComponent } from './admin/admin.component';
import { NavService } from './services/nav.service';
import { MatMenuModule} from '@angular/material/menu';
import { DistributionScheduleComponent, DistributionDialog } from './distribution-schedule/distribution-schedule.component';
import { ViewPrescriptionsComponent } from './view-prescriptions/view-prescriptions.component';
import { PreparationListComponent } from './preparation-list/preparation-list.component';
import { NursingStationComponent, DialogDetails } from './nursing-station/nursing-station.component';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';




@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserComponent,
    NavBarComponent,
    DoctorComponent,
    PharmacistComponent,
    NursesComponent,
    StaffComponent,
    CardComponent,
    ExpandedCardComponent,
    DialogContentExampleDialog,
    AdminComponent,
    DistributionScheduleComponent,
    ViewPrescriptionsComponent,
    PreparationListComponent,
    NursingStationComponent,
    DialogDetails,
    NursesDialogue,
    DistributionDialog
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CustomMaterialModule,
    FormsModule,
    AppRoutingModule,
    HttpModule,
    MatMenuModule,
    NgxMatSelectSearchModule
    
    

  ],
  providers: [LoginService, NavBarComponent, NavService, PrescriptionsService], 
  entryComponents: [
    DialogContentExampleDialog, DialogDetails, NursesDialogue, DistributionDialog
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
