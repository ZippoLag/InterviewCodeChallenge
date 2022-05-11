using CompleteExample.Entities.DTOs;
using System.Linq;

namespace CompleteExample.Logic
{
    public interface IInstructorLogic
    {
        public IQueryable<GradeDTO> GetGivenStudentGrades(int instructorId);
    }
}
