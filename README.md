# ASP.NET Core School CMS

This is a CMS project for a school. The entities includes information about **Students**, **Teachers** and **Courses**.

The MVP includes the CRUD (Create, Read, Update, Delete) functionality for the School Database and integrates API endpoints, server-rendered views, and dynamic routing.

## The main components are:

### 1. Database Connection

The **SchoolDbContext.cs** establish the SQL server.

### 2. API Controller

The Web API Controllers such as **TeacherAPIController.cs**, **SchoolAPIController.cs** and **CourseAPIController.cs** provide endpoints for accessing and modifying the data.

### 3. MVC Controller

The MVC Controllers (**TeacherPageController**, **StudentPageController**, **CoursePageController**) are used for routing to dynamic pages.

### 4. Model

The model classes represent the structure of the tables in the database.

### 5. Views

(_example for teachers_, other follow a similar pattern)

In TeacherPage folder:

- List: Displays the list of all the teachers.

- Show: Display all the information about a particular teacher.

- New: Allows user to add information about a new teacher

- Create: Add a new row in teachers table in database

- ConfirmDelete: Confirm from the user if they really want to delete the record

- Delete: Deletes the record from the database

- Edit: Page that provides user the information about a teacher and allows them to edit it through a form

- Update: Updates the information about the teacher

## Features and Functionality

### API Endpoints

GET /api/teacher/ListTeachers: Returns all teachers.

GET /api/teachers/GiveTeacherInfo/{TeacherId}: Returns a teacher by ID.

POST /api/teacher/AddTeacher: Adds a new teacher.

DELETE /api/teacher/DeleteTeacher/{TeacherId}: Deletes a teacher by ID.

PUT /api/teacher/UpdateTeacher/{TeacherId}, Request body: {Teacher} -> Updates the teacher information
