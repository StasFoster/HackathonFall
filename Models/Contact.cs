using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Models
{
    public class Contact
    {
        [Display(Name = " ")]
        public string Name { get; set; }

        [Display(Name = " ")]
        public string SurName { get; set; }

        [Display(Name = " ")]
        public string Login { get; set; }

        [Display(Name = " ")]
        public string Password { get; set; }

        [Display(Name = " ")]
        public string Message { get; set; }
    }
   
}

