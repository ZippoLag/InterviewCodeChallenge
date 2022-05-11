using CompleteExample.Entities.DTOs;
using System.Collections.Generic;

namespace CompleteExample.Logic
{
    public interface IEnrollmentLogic
    {
        public EnrollmentDTO GetById(int enrollmentId);
        public EnrollmentDTO CreateEnrollment(EnrollmentDTO newEnrollment);
    }
}
