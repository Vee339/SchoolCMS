# ASP.NET Core School CMS

This a project based on the database of a school which includes information about **Students**, **Teachers** and **Courses**.

The MVP includes the CRUD (Create, Read, Update, Delete) functionality for the School Database and integrates API endpoints, server-rendered views, and dynamic routing.

## The main components are: 

### 1. Database Connection

The **SchoolDbContext.cs** establish the SQL server.

### 2. API Controller

The Web API Controller **TeacherAPIController.cs** provide endpoints for accessing the teachers information.

### 3. MVC Controller

A MVC Controller **TeacherPageController.cs** is used for routing to dynamic pages.

### 4. Model 

A model class representing the structure of a table in the database.

### 5. Views

- List View: Displays the list of all the teachers.

- Show View: Display all the information about a particular teacher.


## Features and Functionality

### API Endpoints

GET /api/teachers: Returns all teachers.

GET /api/teachers/{id}: Returns a teacher by ID.

POST /api/teachers: Adds a new teacher.

PUT /api/teachers/{id}: Updates an existing teacher.

DELETE /api/teachers/{id}: Deletes a teacher by ID.

### Dynamic Views

Teacher List Page (/Teacher/List): Server-rendered page displaying all teachers.

Teacher Detail Page (/Teacher/Show/{id}): Server-rendered page showing details for a specific teacher.
