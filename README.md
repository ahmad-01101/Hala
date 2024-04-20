# Hala
## App Testing
App URL ---> [[[http://halatest-001-site1.jtempurl.com <br />](http://ahmaddev-003-site1.gtempurl.com/)](https://ahmaddev-003-site1.gtempurl.com/)](https://ahmaddev-003-site1.gtempurl.com/)

PDF user guide link --> https://drive.google.com/file/d/1s6VtIMUKTzjsuD3c5HTsN7cJhZYSsSnI/view?usp=sharing
> [!NOTE]
> - I used free hosting to host this app so you might encounter slow performance
> - I couldn't use better hosting like Microsoft Azure(pay as you go) because it requires debit card and I dont have it
> - Use the below credentials to use the application

email: testadmin@hala.com \
password: Testadmin@123 \
to access the admin dashboard after you login with above credentials from sidebar click on admin button and you will be redirected to admin dashboard

and to access the user screen you could also use the following credentials: \
email: testuser@hala.com \
password: Testuser123@ 

## Overview
Hala is a complete attendance system developed to enable the employees check in-out, Justify the delay or early out, and view attendances history. It also allows controlling and monitoring by managers/admins.
## Key Features
:white_check_mark: The application is fully responsive\
:white_check_mark: Authentication (login and signup)\
:white_check_mark: Check-in\
:white_check_mark: Check-out\
:white_check_mark: Successful messages after checking in/out\
:white_check_mark: View My attendance\
:white_check_mark: Double check-in operation or double check-out is prevented\
:white_check_mark: User cannot check-out without checking-in first\
:white_check_mark: Any check-in after 8:30 AM will be considered late and must be justified with reason\
:white_check_mark: Any check-out before 3:00 PM will be considered early must be justified with reason\
:white_check_mark: add new employees (admin)\
:white_check_mark: update employees detail (admin)\
:white_check_mark: delete employees (admin)\
:white_check_mark: Successful messages after adding, updating, or deleting employees\
:white_check_mark: view employee all employees attendance (admin)\

## Instalation
### You must have the following in order to use this app locally:
+ visual studio IDE
+ SQL Server Management Studio (SSMS) latest version
+ .NET-6 SDK
### step 1
clone the project to your local device
### step 2
Launch the project solution, go to the package manager console, and enter the following command
```
update-database
```
### step 3
Launch the app and you will be redirected to login page navigate to signup page from the link below the login form then signup as new user
### step 4
open sql and run the following queries

before running this query go to AspNetUsers table you will find only one record with a unique Guid identifier (Id) copy that Id and assign it to the id in the below sql query then run this query and you are now approved user to use Hala system
```
UPDATE [AspNetUsers]
	SET IsApproved = 'true'
WHERE Id ='paste here the id you copied';
```
### step 5
if you want the admin authority then do the following:
create a new role named "admin" by running the following sql query
```
INSERT INTO [AspNetRoles] (Id, Name, NormalizedName)
VALUES (1, 'admin', 'admin');
```
then assign any user to this role by running the following sql query

before running this query you need to get the id of the user you want to assign it to admin role from the AspNetUsers table
```
INSERT INTO [AspNetUserRoles] (UserId, RoleId)
VALUES ('paste here the id of the user you want to assign it to admin role', 1);
```
and you are now have the admin authority 
to access admin features from sidebar click on admin button and you will be redirected to admin dashboard
