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
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentLogic _enrollmentLogic;

        public EnrollmentController(IEnrollmentLogic enrollmentLogic)
        {
            _enrollmentLogic = enrollmentLogic;
        }

        //POST api/enrollments
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EnrollmentDTO> CreateEnrollment(EnrollmentDTO enrollmentDto)
        {
            //TODO: differentiate errors (will be done when middleware is implemented)
            if (enrollmentDto == null || enrollmentDto.EnrollmentId != null)
            {
                return BadRequest();
            }

            var newEnrollment = _enrollmentLogic.CreateEnrollment(enrollmentDto);

            return CreatedAtAction(nameof(ReadEnrollment), new { enrollmentId = newEnrollment.EnrollmentId }, newEnrollment);
        }

        //GET api/enrollments/1
        [HttpGet("{enrollmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EnrollmentDTO> ReadEnrollment(int enrollmentId)
        {
            var enrollment = _enrollmentLogic.GetById(enrollmentId);

            if (enrollment != null)
            {
                return enrollment;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
