//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRIS_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Company
    {
        public Company()
        {
            this.Employees = new HashSet<Employee>();
            this.Positions = new HashSet<Position>();
        }
    
        public int idCompany { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
