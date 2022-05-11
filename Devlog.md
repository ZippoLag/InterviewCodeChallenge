# Intro

Not that this is any kind of usual practice, but I usually keep some kind of "TO-DO" file in my local machine that never sees the light of day, but since it's been a day I've done a "personal" project I'll allow myself to leave a mix of changelog and to-do list over here.

# Design Choices

It's always tempting to over-engineer a solution in hopes to appear clever, but given the fact I have little time to complete this exercise I will try and use the given boilerplate as guidance of what's expected to be fleshed-out and how.

In a similar note, I also felt compelled to write all CRUD operations for the given entities, but for starters I'll limit myself to what's actually being asked for in the description. There's no "simple" way to implement just some HatEoAS support, so I'll declare it out of scope.

As I've spent most of my career maintaining (often uglily architected) legacy applications, I've never had the chance to work in a TDD fashion, but I'll try to keep UTs in mind as much as possible for this exercise.

I've also foregone the need for Interfaces where they weren't crucial. I'll probably won't be needing Automapper either, though I'll probably use Swagger to ease endpoint testing before installing Postman. I'm not implementing Logging for now (though I may at the very least use dependency injection to grab an ILogger if needed).


## TO-DO:

* List all students that have the top 3 grades for each course
	- Implement Unit Tests for GetTopStudentsForAllCourses
* Enroll a student in a course
	- Implement Unit Tests for Read Enrollment
	- Implement Unit Tests for Create Enrollment
* Update a grade(number) for a student for a course
	- Implement Controller and Logic for updating a new Grade for a given CourseId on Student's Controller and Logic
* Stretch Goal: Modify the databse to allow for the storage of students' historical grades.
	- Add Grade entity and update DB Schema
	- Migrate Grades from Enrollment into Grade Table
	- Update all Controllers and Logic methods that deal with GradeDTO and the Grade property from Enrollment so they now relate to the Grade table
	- Remove Grade from Enrollment entity and update DB Schema
* Stretch Goal: Setup a postman collection that calls each one of the endpoints.
* Personal stretch: add error-handling middleware for all endpoints
* Personal stretch: add plural to "Enrollment" in CompleteExampleDBContext.cs
* Personal stretch: replace hardcoded "LocalDevelopmentContext" on Startup.cs and Unit Tests with a setting
* Personal stretch: replace real DB connection on Unit Tests with an in-memory context mock
* Personal stretch: contemplate adding Controller Unit Tests
* Personal stretch: increase GetTopStudentsForAllCourses in StudentLogic.cs

# Done

* For a particular instructor, list all the students' grades the instructor has given out


## Changelog (reverse order)

- Modified Enrollment entity to accept null Grades to match with DB schema
- Implemented Read Enrollment
- Implemented Create Enrollment
- Add Controller and Logic files for Enrollment
- Add Controller and Logic files for Students with a "GetTopStudentsForAllCourses" endpoint which has "3" as default parameter
- Add InstructorLogicTest file and implement GetGivenStudentGrades UTs for cases:
	1. Instructor has given at least one grade for one student
	2. Instructor has Course assigned, but it doesn't have any enrolled students
	Note: there are no Courses without enrolled students in the DB
- Implemented GradeDTO and reworked Controller and Logic
- Add Instructor Logic and implement GetGivenStudentGrades method on Logic	
- Add Instructor Controller and implement GivenStudentGrades GET Method on Controller 
- Installed Swagger for ease of testing
- Added new deafault connection string so we can work with windows authentication on localhost
- Added this file, analyzed the given skeleton and planned-out what needs to be fleshed-out enough to get started
- Installed SQL Server 2016 and tools (already had VS 2019 with .Net Core 3.1)
- Analyzed current project structure and task next steps
