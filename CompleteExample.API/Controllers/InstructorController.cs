using CompleteExample.Entities.DTOs;
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

        //GET instructor/20/given-grades
        [HttpGet("{instructorId}/given-grades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<GradeDTO>> GetGivenStudentGrades(int instructorId)
        {
            return _instructorLogic.GetGivenStudentGrades(instructorId).ToArray();
        }
    }
}
