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
        public ActionResult<IEnumerable<GradeDTO>> GetTopStudentGradesForAllCourses(int podiumSize = 3)
        {
            return _studentLogic.GetTopStudentGradesForAllCourses(podiumSize).ToArray();
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

        //GET api/students/1/enrolled-course-grades
        [HttpGet("{studentId}/enrolled-course-grades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<GradeDTO>> GetGradesForStudent(int studentId)
        {
            return _studentLogic.GetGradesForStudent(studentId).ToArray();
        }

        //GET api/students/1/enrolled-course-grades/4
        [HttpGet("{studentId}/enrolled-course-grades/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<GradeDTO>> GetGradesForStudentByCourse(int studentId, int courseId)
        {
            return _studentLogic.GetGradesForStudentByCourse(studentId, courseId).ToArray();
        }

        //PATCH api/students/1/enrolled-course-grades/4
        [HttpPatch("{studentId}/enrolled-course-grades/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<GradeDTO> UpdateStudentGrade(int studentId, int courseId, [FromBody] decimal newGrade)
        {
            return _studentLogic.UpdateStudentGrade(studentId, courseId, newGrade);
        }
    }
}
