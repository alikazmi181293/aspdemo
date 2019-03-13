using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Models.Data_Transfer_Objects
{
    public class UserDto
    {
        public string Id { set; get; }

        [Required]
        [MinLength(3),MaxLength(50),StringLength(60)]
        public string Name { set; get; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; }
    }
}