# pillar
Pillar is an example of a Customer Support Ticketing System provided through the use of a RESTful API. The backend is built in C# .Net Core 2.2 and the frontend was built using Angular 8. 

![Agent Dashboard](https://i.imgur.com/4zZrHS9.png)  

* [General](#) 
  * [C# .NET Core](#)  
  * [Swagger](#)  
  * [Postgres](#)  
  * [Angular 8](#)  
* [Deployment](#)
  * [Docker](#)  
* [Setup Guide](#)
  * [Prerequisites](#) 
  * [Setup Instructions](#)
  * [Troubleshooting](#)
* [User Guide](#)
  * [Prerequisites](#)
  * [What is Pillar?](#)
  * [Creating an Account](#)
  * [Deleting Accounts, Tickets, or Comments](#)
  * [Is my password safe?](#)
  * [Submitting a Ticket](#)
  * [Commenting on a Ticket](#)
  * [Where to find my app version?](#)
  * [Using the API, Bearer Tokens](#)
  * [What does Status mean?](#)
  * [User Dashboard](#)
  * [Agent Dashboard](#)
  * [What does Priority mean?](#)
  * [What does Type mean?](#)
  * [Creating an Admin account](#)
  * [Reporting](#)
* [Contributing](#)
* [Questions/ Support](#)

# General
## C# .NET Core
## Swagger
## Postgres
## Angular 8

# Deployment
## Docker

# Setup Guide
## Prerequisites?
In order to run Pillar locally there are three prerequisites. 
  
Install Node.js:  
Download Node from https://nodejs.org/en/  
Run the Node.js installer . 
  
Install .NET Core:  
Download .NET SDK 2.2.402 (.NET Core Installer, x64) from https://dotnet.microsoft.com/download/dotnet-core/2.2 . 
Run the .NET Core installer . 
  
Install Angular CLI:  
Open the Terminal (Mac) or CMD (Windows) application on your computer.  
Type the following command: npm install -g @angular/cli . 
  
## Setup Instructions
The following steps should be taken in order to get the Pillar application running locally.  
  
Download and Unzip the Pillar.zip file.  
Open your command line tool (Terminal on Mac or CMD on Windows) and navigate to the Pillar root directory (cd ~/downloads/pillar/pillar). If you run the following the dir or ls command you should see the following directories in the root folder:  
  
Run the following command: cd pillar.api && dotnet run   
Open a new terminal/ cmd window (leave the current window running)  
Repeat Step 2 (Navigate to the Pillar root directory)  
Run the following command: cd pillar.ui && ng serve   
Visit https://localhost:5001/swagger/index.html to view the API page (Google Chrome is the suggested browser).  
Visit http://localhost:4200/ to visit the Front End application where you can log in.  
  
Once you are on the application login page you can create a new account by clicking on the register link or log in with the admin account. The username for the admin account is admin and the password is admin.  
  
## Troubleshooting  
Should you run into any issues along the way, you can try the following:  
  
Local only, your connection isn’t private when accessing API:  
In this case, simply click advanced and then visit the website anyway. This is happening because you are connecting to a live database on google cloud with a local application.  
  
SDK Error while running .NET Core run command:  
Try installing the additional Runtime here: https://dotnet.microsoft.com/download/dotnet-core/2.2  
  
Windows: Runtime & Hosting Bundle   
Mac: NET Core Installer (64)  
  
Permission Error installing Angular CLI  
You may experience an error while installing the Angular CLI if you do not have administrative permissions on your terminal/ cmd. In order to fix this:  

Windows: Run the CMD as Administrator by right clicking on the icon and clicking ‘Run as Administrator’.  
Mac: Add ‘sudo’ to the beginning of the command in the terminal so that the command reads:  
sudo npm install -g @angular/cli  

# User Guide
## What is Pillar?
## Creating an Account
## Deleting Accounts, Tickets, or Comments
## Is my password safe?
## Submitting a Ticket
## Commenting on a Ticket
## Where to find my app version?
## Using the API, Bearer Tokens
## What does Status mean?
## User Dashboard
## Agent Dashboard
## What does Priority mean?
## What does Type mean?
## Creating an Admin account
## Reporting
  
## Contributing
- This project is not currently accepting contributions.

## Questions/ Support
If you have any questions or concerns about this program, please contact me.
