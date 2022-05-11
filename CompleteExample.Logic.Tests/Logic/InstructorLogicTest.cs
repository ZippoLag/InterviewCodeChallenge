using CompleteExample.Entities;
using NUnit.Framework;
using Moq;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CompleteExample.Entities.DTOs;
using System.Collections.Generic;
using NSubstitute;
using System.Linq;


namespace CompleteExample.Logic.Tests
{
    public class InstructorLogicTest
    {
        private CompleteExampleDBContext _context;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            //TODO: replace real DB connection on Unit Tests with an in-memory context mock
            var config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            var optionBuilder = new DbContextOptionsBuilder<CompleteExampleDBContext>();
            optionBuilder.UseSqlServer(config.GetConnectionString("LocalDevelopmentContext"));
            _context = new CompleteExampleDBContext(optionBuilder.Options);
        }

        [Test]
        public void GetGivenStudentGrades_ReturnsNotEmptyQueryable_WhenStudentsAreAssignedToInstructorsCourse()
        {
            //Arrange
            const int SOME_INSTRUCTOR_ID_WITH_GRADES = 20;
            var instructorLogic = new InstructorLogic(_context);

            //Act
            var result = instructorLogic.GetGivenStudentGrades(SOME_INSTRUCTOR_ID_WITH_GRADES);

            //Assert
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetGivenStudentGrades_ReturnsEmptyQueryable_InstructorsCoursesHaveNoStudentsEnrolled()
        {
            //Arrange
            const int SOME_INSTRUCTOR_ID_WITOUT_STUDENTS = 4;
            var instructorLogic = new InstructorLogic(_context);

            //Act
            var result = instructorLogic.GetGivenStudentGrades(SOME_INSTRUCTOR_ID_WITOUT_STUDENTS);

            //Assert
            Assert.IsEmpty(result);
        }
    }
}