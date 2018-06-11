using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRIS_Project.Models
{
    public class EmployeeCl
    {
        [Key]
        public int IdEmployee { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public int IdUser { get; set; }



        public List<Employee> EmployeeList { get; set; }
        public Employee SelectedEmployee { get; set; }


        public string DisplayMode { get; set; }

        public int IdPosition { get; set; }
        public IEnumerable<SelectListItem> PositionName { get; set; }
        public virtual Position Position { get; set; }
    }

    public class Positions
    {
        public int idPosition { get; set; }

        [DisplayName("Select Position")]
        [Required]
        public string PositionName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }


}