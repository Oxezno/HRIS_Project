using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRIS_Project.Models
{
    public class MessagesCl
    {
        [Key]
        public int idMessage { get; set; }
        public string Subject { get; set; }
        public string Message1 { get; set; }
        public string DateSend { get; set; }


        public List<Message> MessageList { get; set; }
        public Message SelectedMessage { get; set; }

        public string DisplayMode { get; set; }

        public int IdUser { get; set; }
        public IEnumerable<SelectListItem> NameUsers { get; set; }

        public int IdEmployee { get; set; }
        public IEnumerable<SelectListItem> NameEmployees { get; set; }


        public virtual Recruitment Recruiter { get; set; }
    }

    public class Users
    {
        public int IdUser { get; set; }

        [DisplayName("Select User")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Recruitment> NameUser { get; set; }
    }
}