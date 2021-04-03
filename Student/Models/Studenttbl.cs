using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Student.Models
{
    public partial class Studenttbl
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Dept { get; set; }
        [Required]
        public int? Rollno { get; set; }
    }
}
