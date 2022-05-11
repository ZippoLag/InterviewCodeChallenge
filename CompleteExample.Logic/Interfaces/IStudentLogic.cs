using CompleteExample.Entities.DTOs;
using System.Collections.Generic;

namespace CompleteExample.Logic
{
    public interface IStudentLogic
    {
        public IEnumerable<GradeDTO> GetTopStudentsForAllCourses(int podiumSize);
    }
}
