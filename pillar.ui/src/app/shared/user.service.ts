import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http"

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder, private http: HttpClient) { }

  readonly BaseUri = 'http://localhost:5000';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':'Bearer '+localStorage.getItem('bearer')
    })
  }

  createTicketFormModel = this.fb.group(
    {
      subject: ['', Validators.required],
      priority: ['', Validators.required],
      type: ['', Validators.required],
      description: ['', Validators.required]
    }
  );

  createTicket(user){
    var ticket = {
      user: {
        userId: user.userId,
        userName: user.userName,
        organization: user.organization,
        requester: user.requester,
        title: user.title
      },
      ticketId: 0,
      subject: this.createTicketFormModel.value.subject,
      description: this.createTicketFormModel.value.description,
      status: "New",
      agent: "",
      priority: this.createTicketFormModel.value.priority,
      type: this.createTicketFormModel.value.type,
      openDate: new Date(Date.now()),
      firstReplyDate: new Date(Date.now()),
      solveDate: new Date(Date.now()),
      reopenDate: new Date(Date.now()),
      finalSolveDate: new Date(Date.now()),
      dueDate: new Date(Date.now()),
      addDate: new Date(Date.now()),
      updateDate: new Date(Date.now()),
    };

    let serializedTicket = JSON.stringify(ticket);
    return this.http.post(this.BaseUri+'/tickets', serializedTicket, this.httpOptions)
  }


  ticketFormModel = this.fb.group(
    {
      // [PUT] /tickets/update
      priority: [''],
      status: [''],
      subject: [''],
      type: [''],
      // [POST] /comments
      message: [''],
    }
  );

  updateTicketForm(t){
    this.ticketFormModel = this.fb.group(
      {
        // [PUT] /tickets/update
        priority: [t.priority],
        status: [t.status],
        type: [t.type],
        // [POST] /comments
        message: [''],
      }
    );
  }

  updateTicket(t, id){
    var ticket = {
      user: {
        userId: t.user.userId,
        userName: t.user.userName,
        organization: t.user.organization,
        requester: t.user.requester,
        title: t.user.title
      },
      userId: id,
      ticketId: t.ticketId,
      subject: t.subject,
      description: t.description,
      status: this.ticketFormModel.value.status,
      agent: t.agent,
      priority: this.ticketFormModel.value.priority,
      type: this.ticketFormModel.value.type,
      openDate: t.openDate,
      firstReplyDate: t.firstReplyDate,
      solveDate: t.solveDate,
      reopenDate: t.reopenDate,
      finalSolveDate: t.finalSolveDate,
      dueDate: t.dueDate,
      addDate: t.addDate,
      updateDate: new Date(Date.now()),
    };

    let serializedTicket = JSON.stringify(ticket);
    return this.http.put(this.BaseUri+'/tickets/update', serializedTicket, this.httpOptions)
  }

  addComment(ticketId, user){
    var comment = {
      userId: user.userId,
      ticketId: ticketId,
      message: this.ticketFormModel.value.message || 'Ticket #' +ticketId+ " has been updated without a comment. The priority, status or ticket type may have been updated.",
      postedDate: new Date(Date.now()),
      addDate: new Date(Date.now()),
      updateDate: new Date(Date.now()),
      user: {
        userName: user.userName,
        organization: user.organization,
        requester: user.requester,
        title: user.title
      }
    };
    let serializedComment = JSON.stringify(comment);
    return this.http.post(this.BaseUri+'/comments', serializedComment, this.httpOptions)
  }
  
  loginFormModel = this.fb.group(
    {
      userName :['', Validators.required],
      password :['', Validators.required],
    }
    );

    login(){
      var user = {
        requester: this.loginFormModel.value.userName,
        password: this.loginFormModel.value.password,
      };
  
      return this.http.get(this.BaseUri+'/users/login/'+user.requester+'/'+user.password)
    }

  registerFormModel = this.fb.group(
  {
    userName :['', Validators.required],
    password :['', Validators.minLength(4)],
    organization :['', Validators.required],
    requester :['', Validators.required],
    title :['', Validators.required]
  });

  register(){
    var body = {
      requester: this.registerFormModel.value.userName,
      userName: this.registerFormModel.value.name,
      password: this.registerFormModel.value.password,
      organization: this.registerFormModel.value.organization,
      title: this.registerFormModel.value.title,
    };
    
    let form = this.registerFormModel.getRawValue();
    let serializedForm = JSON.stringify(form);

    return this.http.post(this.BaseUri+'/users', serializedForm, this.httpOptions)
  }

  getNewTickets(){
    return this.http.get(this.BaseUri+'/tickets/status/count/New', this.httpOptions)
  }

  getPendingTickets(){
    return this.http.get(this.BaseUri+'/tickets/status/count/Pending', this.httpOptions)
  }

  getOpenTickets(){
    return this.http.get(this.BaseUri+'/tickets/status/count/Open', this.httpOptions)
  }

  getSolvedTickets(){
    return this.http.get(this.BaseUri+'/tickets/status/count/Solved', this.httpOptions)
  }

  getTicketsUserDashboard(){
    return this.http.get(this.BaseUri+'/tickets', this.httpOptions)
  }

  getTicketsByStatus(type){
    return this.http.get(this.BaseUri+'/tickets/Status/'+type, this.httpOptions)
  }

  getTicketById(id){
    return this.http.get(this.BaseUri+'/tickets/'+id, this.httpOptions)
  }

  getCommentById(id){
    return this.http.get(this.BaseUri+'/comments/ticket/'+id, this.httpOptions)
  }

  getUserInfo(id){
    return this.http.get(this.BaseUri+'/users/'+id, this.httpOptions)
  }

  getOpenTicketsByUser(id){
    return this.http.get(this.BaseUri+'/tickets/status/Open/user/'+id, this.httpOptions)
  }

  getNewTicketsByUser(id){
    return this.http.get(this.BaseUri+'/tickets/status/New/user/'+id, this.httpOptions)
  }

  getPendingTicketsByUser(id){
    return this.http.get(this.BaseUri+'/tickets/status/Pending/user/'+id, this.httpOptions)
  }

  getSolvedTicketsByUser(id){
    return this.http.get(this.BaseUri+'/tickets/status/Solved/user/'+id, this.httpOptions)
  }
  
}