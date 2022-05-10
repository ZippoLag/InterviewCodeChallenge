using CompleteExample.Entities;
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
        public IQueryable<Enrollment> GetGivenStudentGrades(int instructorId)
        {
            var courseIds = _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .Select(c => c.CourseId);

            var grades = _context.Enrollment
                .Where(e => courseIds.Contains(e.CourseId));

            return grades;

        }
    }
}
