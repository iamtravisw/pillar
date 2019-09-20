import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from "@angular/forms"
import { HttpClient, HttpClientModule } from "@angular/common/http"
import { GoogleChartsModule } from 'angular-google-charts';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './agent-dashboard//sidebar/sidebar.component';
import { ActivitiesComponent } from './agent-dashboard/activities/activities.component';
import { SummaryComponent } from './agent-dashboard/summary/summary.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { UserService } from './shared/user.service';
import { NewComponent } from './tickets/new/new.component';
import { TicketsComponent } from './tickets/tickets.component';
import { AgentDashboardComponent } from './agent-dashboard/agent-dashboard.component';
import { OpenComponent } from './tickets/open/open.component';
import { PendingComponent } from './tickets/pending/pending.component';
import { SolvedComponent } from './tickets/solved/solved.component';
import { ReportsComponent } from './reports/reports.component';
import { MoreComponent } from './more/more.component';
import { VersionComponent } from './more/version/version.component';
import { DocumentationComponent } from './more/documentation/documentation.component';
import { TicketComponent } from './tickets/ticket/ticket.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { UserSidebarComponent } from './user-dashboard/user-sidebar/user-sidebar.component';
import { OpenedComponent } from './user-dashboard/opened/opened.component';
import { ClosedComponent } from './user-dashboard/closed/closed.component';
import { CreateComponent } from './user-dashboard/create/create.component';
import { TicketByStatusComponent } from './reports/ticket-by-status/ticket-by-status.component';
import { TicketByCustomerComponent } from './reports/ticket-by-customer/ticket-by-customer.component';
import { TicketTableComponent } from './reports/ticket-table/ticket-table.component';
import { FilterPipe } from './more/documentation/filter.pipe';

library.add(fas);

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    SidebarComponent,
    ActivitiesComponent,
    SummaryComponent,
    LoginComponent,
    RegistrationComponent,
    UserComponent,
    NewComponent,
    TicketsComponent,
    AgentDashboardComponent,
    OpenComponent,
    PendingComponent,
    SolvedComponent,
    ReportsComponent,
    MoreComponent,
    VersionComponent,
    DocumentationComponent,
    TicketComponent,
    UserDashboardComponent,
    UserSidebarComponent,
    OpenedComponent,
    ClosedComponent,
    CreateComponent,
    TicketByStatusComponent,
    TicketByCustomerComponent,
    TicketTableComponent,
    FilterPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpClientModule,
    GoogleChartsModule,
    FormsModule,
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})


export class AppModule { 
  
}
