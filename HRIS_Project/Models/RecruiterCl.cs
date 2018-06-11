using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRIS_Project.Models
{
    public class RecruiterCl
    {
        [Key]
        public int idUser { get; set; }
        public string Name { get; set; }
        public string Paternal { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DateCreate { get; set; }
        public Boolean isAdmin { get; set; }

        public List<Recruitment> RecruiterList { get; set; }
        public Recruitment SelectedRecruiter { get; set; }

        public string DisplayMode { get; set; }
    }
}