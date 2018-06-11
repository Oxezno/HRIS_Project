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
    
    public partial class Employee
    {
        public Employee()
        {
            this.Messages = new HashSet<Message>();
        }
    
        public int IdEmployee { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public Nullable<int> idCompany { get; set; }
        public Nullable<int> idUser { get; set; }
        public Nullable<int> idPosition { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Recruitment Recruitment { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual Position Position { get; set; }
    }
}
