using CompleteExample.Entities;
using CompleteExample.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteExample.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorLogic _instructorLogic;

        public InstructorController(IInstructorLogic instructorLogic)
        {
            _instructorLogic = instructorLogic;
        }

        [HttpGet("{instructorId}/enrollments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Enrollment>> GetGivenStudentGrades(int instructorId)
        {
            return _instructorLogic.GetGivenStudentGrades(instructorId).ToArray();
        }
    }
}
