# Intro

Not that this is any kind of usual practice, but I usually keep some kind of "TO-DO" file in my local machine that never sees the light of day, but since it's been a day I've done a "personal" project I'll allow myself to leave a mix of changelog and to-do list over here.

# Design Choices

It's always tempting to over-engineer a solution in hopes to appear clever, but given the fact I have little time to complete this exercise I will try and repeat "Premature Optimization is the Root of All Evil" as a mantra (OK, it's not the same, but close enough). As such, I won't be, for example adding a "Service" Project to sit between the Controller and Logic projects, instead having less-than-thin Controllers which will be handling the mapping among DTO and Entity classes (which means I consider DTOs essential I guess?), though I'm very DRY oriented so I may be implementing a Middleware for error handling if I have the time. 

In a similar note, I also felt compelled to write all CRUD operations for the given entities, but for starters I'll limit myself to what's actually being asked for in the description.

TBH I've spent most of my career maintaining (often uglily architected) legacy applications, so I've never had the chance to work in a TDD fashion, but I'll try to keep UTs in mind as much as possible for this exercise.

I've also foregone the need for Interfaces where they weren't crucial. I will see if I need AutoMapper for mappings, I will probably use Swagger to ease endpoint testing before installing Postman. I'm not implementing Logging for now (though I may at the very least use dependency injection to grab an ILogger if needed).


## TO-DO:

- Analyze current project structure and task next steps
* For a particular instructor, list all the students' grades the instructor has given out
	- Add InstructorLogicTest file and implement GetGivenStudentGrades UTs for cases:
		1. Instructor has given at least one grade for one student
		2. Instructor has no Course assigned
		3. Instructor has Course assigned, but it doesn't have any enrolled students
	- Implement StudentGradesDTO and mapper for the Controller's Response
	- Add InstructorControllerTest file and implement Uts (TBD)
* List all students that have the top 3 grades for each course
* Enroll a student in a course
* Update a grade(number) for a student for a course
* Stretch Goal: Modify the databse to allow for the storage of students' historical grades.
* Stretch Goal: Setup a postman collection that calls each one of the endpoints.
* Stretch Refactor: add plural to "Enrollment" in CompleteExampleDBContext.cs


## Changelog (reverse order)

- Add Instructor Logic and implement GetGivenStudentGrades method on Logic	
- Add Instructor Controller and implement GivenStudentGrades GET Method on Controller 
- Installed Swagger for ease of testing
- Added new deafault connection string so we can work with windows authentication on localhost
- Added this file, analyzed the given skeleton and planned-out what needs to be fleshed-out enough to get started
- Installed SQL Server 2016 and tools (already had VS 2019 with .Net Core 3.1)
