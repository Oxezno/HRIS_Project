using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRIS_Project.Models
{
    public class CompaniesCl
    {
        [Key]
        public string idCompany { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string idPosition { get; set; }

        public List<Company> CompaniesList { get; set; }
        public Company SelectedCompany { get; set; }
        public string DisplayMode { get; set; }
    }
}