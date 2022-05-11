using CompleteExample.Entities;
using CompleteExample.Entities.DTOs;
using System.Linq;
using System.Collections.Generic;
using System;

namespace CompleteExample.Logic
{
    public class EnrollmentLogic : IEnrollmentLogic
    {
        private readonly CompleteExampleDBContext _context;

        public EnrollmentLogic(CompleteExampleDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets an EnrollmentDTO with basic details of related entities by its Id.
        /// </summary>
        /// <param name="enrollmentId">Id of the requested Enrollment</param>
        /// <returns>EnrollmentDTO with basic details of related entities</returns>
        public EnrollmentDTO GetById(int enrollmentId)
        {
            var enrollment = _context.Enrollment.FirstOrDefault(e => e.EnrollmentId == enrollmentId);

            if (enrollment != null)
            {
                return MapEnrollmentToDTO(enrollment);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a new Enrollment for a Student in a Course
        /// </summary>
        /// <param name="newEnrollment"></param>
        /// <returns>The newly created Enrollment with it's DB-assigned Id</returns>
        public EnrollmentDTO CreateEnrollment(EnrollmentDTO newEnrollment)
        {
            var enrollment = _context.Enrollment.FirstOrDefault(e => e.StudentId == newEnrollment.StudentId && e.CourseId == newEnrollment.CourseId);

            if (enrollment != null)
            {
                throw new InvalidOperationException($"An Enrollment already exists for StudentId {newEnrollment.StudentId} on CourseId {newEnrollment.CourseId}.");
            }
            
            enrollment = new Enrollment()
            {
                CourseId = newEnrollment.CourseId,
                StudentId = newEnrollment.StudentId,
                Grade = null
            };

            _context.Enrollment.Add(enrollment);
            _context.SaveChanges();

            enrollment = _context.Enrollment.First(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId);

            return MapEnrollmentToDTO(enrollment);
        }

        private EnrollmentDTO MapEnrollmentToDTO(Enrollment enrollment)
        {
            var enrolledCourse = _context.Courses.FirstOrDefault(c => c.CourseId == enrollment.CourseId);
            var enrolledStudent = _context.Students.FirstOrDefault(s => s.StudentId == enrollment.StudentId);

            var enrollmentDTO = new EnrollmentDTO()
            {
                EnrollmentId = enrollment.EnrollmentId,
                CourseId = enrollment.CourseId,
                Course = enrolledCourse.Title,
                StudentId = enrollment.StudentId,
                Student = $"{enrolledStudent.LastName}, {enrolledStudent.FirstName}",
            };

            return enrollmentDTO;
        }
    }
}
