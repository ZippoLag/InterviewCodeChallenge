using CompleteExample.Entities;
using CompleteExample.Entities.DTOs;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompleteExample.Logic
{
    public class InstructorLogic : IInstructorLogic
    {
        private readonly CompleteExampleDBContext _context;

        public InstructorLogic(CompleteExampleDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// "Grades" are stored in Enrollment entities. There's a future requirement to have multiple grades per enrollment, but we'll cross that bridge when we get to it.
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns>All Enrollments assigned to all Courses taught by an Instructor with Id instructorId</returns>
        public IQueryable<GradeDTO> GetGivenStudentGrades(int instructorId)
        {
            var courses = _context.Courses
                .Where(c => c.InstructorId == instructorId);

            var grades = from e in _context.Enrollment
                         join c in courses 
                            on e.CourseId equals c.CourseId
                         join s in _context.Students 
                            on e.StudentId equals s.StudentId
                         select new GradeDTO() { 
                             CourseId = c.CourseId, 
                             Course = c.Title, 
                             StudentId = s.StudentId, 
                             Student = $"{s.LastName}, {s.FirstName}",
                             Grade = e.Grade
                         };

            return grades;

        }
    }
}
