using CompleteExample.Entities;
using CompleteExample.Entities.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace CompleteExample.Logic
{
    public class StudentLogic : IStudentLogic
    {
        private readonly CompleteExampleDBContext _context;

        public StudentLogic(CompleteExampleDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="podiumSize"></param>
        /// <returns>A flat list of all the top podiumSize grades ordered by Course</returns>
        public IEnumerable<GradeDTO> GetTopStudentsForAllCourses(int podiumSize)
        {
            List<GradeDTO> topGrades = new List<GradeDTO>();

            //TODO: move as much of the query as possible to a single LINQ expression instead of first fetching the whole list of courses
            var courses = _context.Courses.ToArray();
            foreach (Course course in courses)
            {
                topGrades.AddRange(GetTopStudentsForCourse(course.CourseId, podiumSize));
            }

            return topGrades;
        }

        private IEnumerable<GradeDTO> GetTopStudentsForCourse(int courseId, int podiumSize)
        {
            //TODO: avoid using sort to increase performance
            var enrollmentsInPodium = _context.Enrollment
                .Where(e => e.CourseId == courseId)
                .OrderByDescending(e=> e.Grade)
                .Take(podiumSize);

            var podiumGrades = from e in enrollmentsInPodium
                   join c in _context.Courses
                        on e.CourseId equals c.CourseId
                   join s in _context.Students
                      on e.StudentId equals s.StudentId
                   select new GradeDTO()
                   {
                       CourseId = c.CourseId,
                       Course = c.Title,
                       StudentId = s.StudentId,
                       Student = $"{s.LastName}, {s.FirstName}",
                       Grade = (decimal)e.Grade
                   };

            return podiumGrades;
        }
    }
}
