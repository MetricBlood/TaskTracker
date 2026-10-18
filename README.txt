TASK TRACKER
============

A small REST API for managing projects and their tasks, built with .NET 8 and
ASP.NET Core Web API. Built as a take-home assignment for the Junior Fullstack
Developer role at Aventra Group.


TECH STACK
----------
- .NET 8, ASP.NET Core Web API (controllers)
- Swagger UI (Swashbuckle) for API documentation and testing
- In-memory data: a hard-coded List<Project> with sample data, no database


HOW TO RUN
----------
Requirements: .NET 8 SDK

Option 1: Visual Studio
  1. Open TaskTracker.slnx (needs Visual Studio 2022 17.13 or later, or
     Visual Studio 2026)
  2. Select the "https" profile and press F5
  3. Swagger opens at https://localhost:7208/swagger

Option 2: Command line
  cd TaskTracker
  dotnet run --project TaskTrackerApi --launch-profile https

  Then open https://localhost:7208/swagger in a browser.

If the browser shows a certificate warning, run this once and restart:
  dotnet dev-certs https --trust

Note: data is stored in memory, so any changes reset when the app restarts.


ENDPOINTS
---------
Action    Projects                    Tasks
-------   -------------------------   ------------------------------
List      GET    /api/projects        GET    /api/projects/{id}/tasks
Get one   GET    /api/projects/{id}   GET    /api/tasks/{id}
Create    POST   /api/projects        POST   /api/projects/{id}/tasks
Update    PUT    /api/projects/{id}   PUT    /api/tasks/{id}
Delete    DELETE /api/projects/{id}   DELETE /api/tasks/{id}


EXAMPLE REQUESTS
----------------
Create a project:

  {
    "name": "Website Redesign",
    "description": "Refresh the company website"
  }

Create or update a task:

  {
    "title": "Create wireframes",
    "description": "Homepage and contact page",
    "status": "InProgress",
    "dueDate": "2026-10-25T00:00:00Z"
  }

Status accepts Todo, InProgress or Done, and defaults to Todo if left out.


RESPONSE CODES
--------------
200 OK            Successful GET or PUT
201 Created       Successful POST (includes a Location header with the new
                  item's URL)
204 No Content    Successful DELETE
400 Bad Request   Validation failed, for example an empty name or an
                  invalid status
404 Not Found     The project or task id does not exist


SAMPLE DATA
-----------
The app starts with 2 projects and 4 tasks:

Website Redesign
  - Create wireframes (Done)
  - Design homepage mockup (InProgress)
  - Write About Us content (Todo)

Mobile App Launch
  - Fix login bug (Todo)


PROJECT STRUCTURE
-----------------
TaskTracker/
  TaskTracker.slnx
  TaskTrackerApi/
    Controllers/
      ProjectsController.cs     5 project endpoints
      TasksController.cs        5 task endpoints
    Data/
      InMemoryData.cs           Hard-coded list and sample data
    Dtos/
      ProjectRequest.cs         Request body for creating/updating a project
      TaskRequest.cs            Request body for creating/updating a task
    Models/
      Project.cs
      TaskItem.cs
      TaskItemStatus.cs
    Program.cs
    TaskTrackerApi.http         Ready-made test requests for every endpoint


WHAT I COMPLETED
----------------
Required
  Done      .NET 8 Web API with Swagger
  Done      In-memory data with a hard-coded list (2 projects, 4 tasks)
  Done      All 10 CRUD endpoints for projects and tasks
  Done      README

Optional
  Not done  Database
  Not done  Blazor page


DESIGN DECISIONS
----------------
- Controllers instead of minimal APIs: keeps project and task endpoints in
  separate classes, which is easier to read and extend.

- Request DTOs (ProjectRequest, TaskRequest): the client can only send the
  fields it should. Id, CreatedAt and ProjectId are set by the server, so they
  cannot be overwritten through the API.

- Validation with data annotations: name and title are required and have a
  maximum length, and status must be a valid value. [ApiController] returns a
  400 automatically when validation fails.

- Status as text: JsonStringEnumConverter makes the API return "Done" instead
  of 2, which is easier to read.

- Tasks are stored inside their project: there is only one copy of each task,
  and deleting a project also deletes its tasks.

- Singleton data service: InMemoryData is registered as a singleton so data
  persists between requests while the app is running.

- PUT returns the updated item (200) so the change can be seen immediately in
  Swagger.


TESTING
-------
I tested every endpoint in Swagger, including the error cases (404 for a
missing id, 400 for invalid input). The same tests are saved in
TaskTrackerApi/TaskTrackerApi.http and can be run from Visual Studio with the
"Send request" link above each request. Run them from top to bottom, since
some depend on items created earlier in the file.


KNOWN LIMITATIONS
-----------------
- Data is lost when the app restarts (in-memory only).
- List<T> is not thread-safe. This is fine for a demo, but a real app would
  use a database or a concurrent collection.
- No authentication or pagination.


HOW I USED AI
-------------
I used Claude as a tutor, one step at a time rather than generating the whole
project at once. For each step it explained the approach and the code, and I
applied the changes myself, built the project, tested the endpoints in
Swagger, and committed before moving on. The commit history shows each step
separately.
