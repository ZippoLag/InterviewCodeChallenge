using CompleteExample.Entities.DTOs;
using CompleteExample.Entities;
using CompleteExample.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CompleteExample.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic _studentLogic;

        public StudentController(IStudentLogic studentLogic)
        {
            _studentLogic = studentLogic;
        }

        //GET api/students/top-students/3
        [HttpGet("top-students/{podiumSize?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GradeDTO>> GetTopStudentsForAllCourses(int podiumSize = 3)
        {
            return _studentLogic.GetTopStudentsForAllCourses(podiumSize).ToArray();
        }

        //GET api/students/1
        [HttpGet("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> ReadStudent(int studentId)
        {
            var student = _studentLogic.GetById(studentId);

            if (student != null)
            {
                return student;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
