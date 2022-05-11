using CompleteExample.Entities.DTOs;
using CompleteExample.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CompleteExample.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic _studentLogic;

        public StudentController(IStudentLogic studentLogic)
        {
            _studentLogic = studentLogic;
        }

        //GET students/top-students/3
        [HttpGet("/top-students/{podiumSize?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GradeDTO>> GetTopStudentsForAllCourses(int podiumSize = 3)
        {
            return _studentLogic.GetTopStudentsForAllCourses(podiumSize).ToArray();
        }
    }
}
