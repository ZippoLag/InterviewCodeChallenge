using CompleteExample.Entities.DTOs;
using CompleteExample.Entities;
using System.Collections.Generic;

namespace CompleteExample.Logic
{
    public interface IStudentLogic
    {
        public Student GetById(int studentId);
        public IEnumerable<GradeDTO> GetTopStudentsForAllCourses(int podiumSize);
    }
}
