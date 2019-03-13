using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Models.Data_Transfer_Objects
{
    public class CourseDto
    {
        public int Id { set; get; }

        [Required]
        [MinLength(5), MaxLength(40), StringLength(50)]
        public string Name { set; get; }

        [Required]
        [MinLength(5), MaxLength(200), StringLength(200)]
        public string Description { set; get; }

        [Required]
        public int CreditHours { set; get; }
    }
}