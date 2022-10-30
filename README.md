# RiseAndShineMVC_Capstone_Project

Introduction

This is a MVP demonstration of my Server-Side Individual NSS Project

Technologies Used:

![Untitled design](https://user-images.githubusercontent.com/91228783/198887111-062638c2-d000-4044-a3b7-7fcd7a0c6cc1.png)

ERD: [https://dbdiagram.io/d/6282fad97f945876b62dfc13](https://dbdiagram.io/d/6337327b7b3d2034fffad7f8)
Wireframe: [https://sketchboard.me/zCULqeUotfzf](https://sketchboard.me/VCUzmPwhPKOm)

Purpose & motivation for project

The purpose of this app is to provide a simple platform from which vehicle owners can request an @home car washing. Future functionality will include the ability of an owner to be able to generate available service times. A service provider will also have the ability to generate service requests times-slots and accept a request from the owner.

Upon creating a user account, you can log in and see your account details. This view has an 'add vehicle' button option that redirects you to a form where you can add a vehicle to your account. When you click the add button, the user will be redirected to the Account Details view and the car will now be displayed in that view. By default a "Schedule Service" button is added to the newly created car and when clicked will redirect the user to the Service Requests view whcih will display a list of available service requests with time stamp and a dropdown list of the different types of car-washing packages available. Here you choose the desired car-wash package on the service request time-slot that you want and then click the "Schedule Service" button. Upon clicking the button the owner will be redirected back to the Account Details view which now display the vehicle and the service request for that vehicle. The Service Request can be edited and deleted by clicking the repective edit and delete buttons.

The application was developed first with a wireframe visual representation of the the functionality of the app. When the vision for the app was complete an Entity Relationship Diagram was created to show the visual/logical reletaionship between data-points as well as give a guide for the development of the Sql server database.

### Getting Started with the App

#### TO BEGIN FOLLOW THE GUIDELINES PROVIDED
Clone down a template of the Project to your local machine.
This project was developed in MS Visual Studio version 17.3.1, >NET Framework version 4.8.04084
Make sure you have Visual Studio installed.
Install NuGet PackageManager version 6.3.0.
Install Razor (ASP.NET Core) version 17.0..0.2....
Install Sql Server Data Tools version 17.0.62207.04100

After installations are complete, open terminal and cd into the directory where you have cloned down the github project.
## run: start RiseAndShine.sln
The project will open in Visual Studio.
Select the debug button and make sure RiseAndShine is selected and not IIS.
You can choose a default browser from which you would like to display the app by clicking the dropdown arrow next to the right of the debug button.
### The app should now be running and displayed in your browser. 

To stop the app, simply stop the debugging by clicking the debug arrow button. The browser window automatically closes.



