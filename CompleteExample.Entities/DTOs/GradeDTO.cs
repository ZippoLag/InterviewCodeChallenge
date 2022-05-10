using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompleteExample.Entities.DTOs
{
    [NotMapped]
    public class GradeDTO
    {
        public int StudentId { get; set; }
        public string Student { get; set; }
        public int CourseId { get; set; }
        public string Course { get; set; }
        public Decimal Grade { get; set; }
    }
}
