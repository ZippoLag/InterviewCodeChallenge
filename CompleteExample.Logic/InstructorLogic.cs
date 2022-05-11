using CompleteExample.Entities;
using CompleteExample.Entities.DTOs;
using System.Linq;
using System.Collections.Generic;

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
        /// 
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns>An IQueryable with the all the Grades given by an Instructor to all Students in all of their Courses</returns>
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
