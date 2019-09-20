import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';


@Component({
  selector: 'app-documentation',
  templateUrl: './documentation.component.html',
  styleUrls: ['./documentation.component.css']
})
export class DocumentationComponent implements OnInit {

  private fragment: string;

  constructor(private route: ActivatedRoute, private router: Router ) { }


  docs = [
    'T01: What is Pillar?', 
    'T02: Creating an Account',
    'T03: Submitting a Ticket',
    'T04: Commenting on a Ticket',
    'T05: Where to find App Version',
    'T06: Using the API, Bearer Tokens',
    'T07: What does Status mean?',
    'T08: Resetting My Password',
    'T09: User Dashboard Help',
    'T10: Agent Dashboard Help',
    'T11: What does Priority mean?',
    'T12: What does Type mean?',
    'T13: Creating an Admin Account',
    'T14: Using Reports'
  ]

  ngOnInit() {

    this.router.events.subscribe(s => {
      if (s instanceof NavigationEnd) {
        const tree = this.router.parseUrl(this.router.url);
        if (tree.fragment) {
          const element = document.querySelector("#" + tree.fragment);
          if (element) { element.scrollIntoView(true); }
        }
      }
    });
  }
}