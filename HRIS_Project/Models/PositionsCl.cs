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
    public class PositionsCl
    {
        [Key]
        public int idPosition { get; set; }
        public string PositionName { get; set; }
        public string PositionDescription { get; set; }
        public string DateCreate { get; set; }
        public Boolean Status { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int IdUser { get; set; }

        public List<Position> PositionList { get; set; }
        public Position SelectedPosition { get; set; }


        public string DisplayMode { get; set; }

        public int IdCompany { get; set; }
        public IEnumerable<SelectListItem> CompanyName { get; set; }


        public virtual Company Company { get; set; }
    }

    public class CompanyCl
    {
        public int idCompany { get; set; }

        [DisplayName("Select Company")]
        [Required]
        public string CompanyName { get; set; }

        public virtual ICollection<Position> Position { get; set; }
    }
}