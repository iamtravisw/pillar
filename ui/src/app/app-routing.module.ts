import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { NewComponent } from './tickets/new/new.component';
import { TicketsComponent } from './tickets/tickets.component';
import { AgentDashboardComponent } from './agent-dashboard/agent-dashboard.component';
import { PendingComponent } from './tickets/pending/pending.component';
import { OpenComponent } from './tickets/open/open.component';
import { SolvedComponent } from './tickets/solved/solved.component';
import { ReportsComponent } from './reports/reports.component';
import { DocumentationComponent } from './more/documentation/documentation.component';
import { VersionComponent } from './more/version/version.component';
import { TicketComponent } from './tickets/ticket/ticket.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { CreateComponent } from './user-dashboard/create/create.component';
import { OpenedComponent } from './user-dashboard/opened/opened.component';
import { ClosedComponent } from './user-dashboard/closed/closed.component';
import { MoreComponent } from './more/more.component';
import { TicketByStatusComponent } from './reports/ticket-by-status/ticket-by-status.component';
import { TicketByCustomerComponent } from './reports/ticket-by-customer/ticket-by-customer.component';
import { TicketTableComponent } from './reports/ticket-table/ticket-table.component';

const routes: Routes = [
  {
    path:'',redirectTo:'/user/login',pathMatch:'full'
  },
  {
    path: 'user', 
    component: UserComponent,
    children: [
      { 
        path: 'registration', 
        component: RegistrationComponent
      },
      {
        path: 'login', 
        component: LoginComponent
      }
    ]
  },
  { 
    path: 'dashboard', component: AgentDashboardComponent, canActivate:[AuthGuard]
  },
  {
  path: 'tickets', 
    component: TicketsComponent,
    children: [
      { 
        path: 'new', 
        component: NewComponent, canActivate:[AuthGuard]
      },
      { 
        path: 'pending', 
        component: PendingComponent, canActivate:[AuthGuard]
      },
      { 
        path: 'open', 
        component: OpenComponent, canActivate:[AuthGuard]
      },
      { 
        path: 'solved', 
        component: SolvedComponent, canActivate:[AuthGuard]
      },
      { 
        path: ':id', 
        component: TicketComponent, canActivate:[AuthGuard]
      },
    ]
  },
  {
    path: 'user-dashboard', 
      component: UserDashboardComponent,
      children: [
        { 
          path: 'new', 
          component: CreateComponent, canActivate:[AuthGuard]
        },
        { 
          path: 'opened', 
          component: OpenedComponent, canActivate:[AuthGuard]
        },
        { 
          path: 'closed', 
          component: ClosedComponent, canActivate:[AuthGuard]
        },
        { 
          path: 'ticket/:id', 
          component: TicketComponent, canActivate:[AuthGuard]
        },
      ]
    },
  {
  path: 'reports', 
    component: ReportsComponent,
    children: [
      { 
        path: 'status', 
        component: TicketByStatusComponent, canActivate:[AuthGuard]
      },
      { 
        path: 'customer', 
        component: TicketByCustomerComponent, canActivate:[AuthGuard]
      },
      { 
        path: 'table', 
        component: TicketTableComponent, canActivate:[AuthGuard]
      },
    ]
  },
  {
    path: 'more', 
      component: MoreComponent,
      children: [
        { 
          path: 'documentation', 
          component: DocumentationComponent
        },
        { 
          path: 'version', 
          component: VersionComponent
        },
      ]
    },
];

@NgModule({
  imports: [
    RouterModule.forRoot(
    routes,
    { 
    //  enableTracing: true 
    }),
  RouterModule.forChild(
    routes,
    ),  
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
