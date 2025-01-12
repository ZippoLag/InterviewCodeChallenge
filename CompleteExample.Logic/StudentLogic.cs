﻿using CompleteExample.Entities;
using CompleteExample.Entities.DTOs;
using System.Linq;
using System.Collections.Generic;
using System;

namespace CompleteExample.Logic
{
    public class StudentLogic : IStudentLogic
    {
        private readonly CompleteExampleDBContext _context;

        public StudentLogic(CompleteExampleDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a Student by its Id.
        /// </summary>
        /// <param name="studentId">Id of the requested Student</param>
        /// <returns>The Student for the given Id</returns>
        public Student GetById(int studentId)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);

            return student;
        }

        /// <summary>
        /// Gets all the Grades given for a Student in all of their Enrolled Courses
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public IEnumerable<GradeDTO> GetGradesForStudent(int studentId)
        {
            var studentGrades = from e in _context.Enrollment
                                join s in _context.Students
                                  on e.StudentId equals s.StudentId
                                join c in _context.Courses
                                     on e.CourseId equals c.CourseId
                                where e.StudentId == studentId
                                select new GradeDTO()
                                {
                                    CourseId = c.CourseId,
                                    Course = c.Title,
                                    StudentId = s.StudentId,
                                    Student = $"{s.LastName}, {s.FirstName}",
                                    Grade = (decimal)e.Grade
                                };

            return studentGrades;
        }

        /// <summary>
        /// Gets all the Grades given for a Student for one of their Enrolled Courses
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public IEnumerable<GradeDTO> GetGradesForStudentByCourse(int studentId, int courseId)
        {
            var studentGrades = from e in _context.Enrollment
                                join s in _context.Students
                                  on e.StudentId equals s.StudentId
                                join c in _context.Courses
                                     on e.CourseId equals c.CourseId
                                where e.StudentId == studentId && e.CourseId == courseId
                                select new GradeDTO()
                                {
                                    CourseId = c.CourseId,
                                    Course = c.Title,
                                    StudentId = s.StudentId,
                                    Student = $"{s.LastName}, {s.FirstName}",
                                    Grade = (decimal)e.Grade
                                };

            return studentGrades;
        }

        public GradeDTO UpdateStudentGrade(int studentId, int courseId, decimal newGrade)
        {
            var enrollment = _context.Enrollment.FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment == null)
            {
                throw new InvalidOperationException($"The Student with Id {studentId} is not currently Enrolled in the given Course with Id {courseId}.");
            }

            enrollment.Grade = newGrade;
            _context.SaveChanges();

            var enrolledCourse = _context.Courses.FirstOrDefault(c => c.CourseId == enrollment.CourseId);
            var enrolledStudent = _context.Students.FirstOrDefault(s => s.StudentId == enrollment.StudentId);

            return new GradeDTO()
            {
                CourseId = enrollment.CourseId,
                Course = enrolledCourse.Title,
                StudentId = enrollment.StudentId,
                Student = $"{enrolledStudent.LastName}, {enrolledStudent.FirstName}",
                Grade = (decimal)enrollment.Grade
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="podiumSize"></param>
        /// <returns>A flat list of all the top podiumSize grades ordered by Course</returns>
        public IEnumerable<GradeDTO> GetTopStudentGradesForAllCourses(int podiumSize)
        {
            List<GradeDTO> topGrades = new List<GradeDTO>();

            //TODO: move as much of the query as possible to a single LINQ expression instead of first fetching the whole list of courses
            var courses = _context.Courses.ToArray();
            foreach (Course course in courses)
            {
                topGrades.AddRange(GetTopStudentsForCourse(course.CourseId, podiumSize));
            }

            return topGrades;
        }

        private IEnumerable<GradeDTO> GetTopStudentsForCourse(int courseId, int podiumSize)
        {
            //TODO: avoid using sort to increase performance
            var enrollmentsInPodium = _context.Enrollment
                .Where(e => e.CourseId == courseId)
                .OrderByDescending(e=> e.Grade)
                .Take(podiumSize);

            var podiumGrades = from e in enrollmentsInPodium
                   join c in _context.Courses
                        on e.CourseId equals c.CourseId
                   join s in _context.Students
                      on e.StudentId equals s.StudentId
                   select new GradeDTO()
                   {
                       CourseId = c.CourseId,
                       Course = c.Title,
                       StudentId = s.StudentId,
                       Student = $"{s.LastName}, {s.FirstName}",
                       Grade = (decimal)e.Grade
                   };

            return podiumGrades;
        }
    }
}
