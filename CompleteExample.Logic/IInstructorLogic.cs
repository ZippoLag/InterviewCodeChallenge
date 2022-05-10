using CompleteExample.Entities;
using System.Linq;

namespace CompleteExample.Logic
{
    public interface IInstructorLogic
    {
        public IQueryable<Enrollment> GetGivenStudentGrades(int instructorId);
    }
}
