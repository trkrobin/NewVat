using System;

namespace newvat.ViewModels
{
    public class CustomerVM
    {
        public string CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerGroupID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string StartDateTime { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonDesignation { get; set; }
        public string ContactPersonTelephone { get; set; }
        public string ContactPersonEmail { get; set; }
        public string TINNo { get; set; }
        public string VATRegistrationNo { get; set; }
        public string Comments { get; set; }
        public bool ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string Info4 { get; set; }
        public string Info5 { get; set; }
        public string Country { get; set; }
        public decimal VDSPercent { get; set; }
        public string BusinessType { get; set; }
        public string BusinessCode { get; set; }
        public string Operation { get; set; }
        public string CustomerGroup { get; set; }
        public bool IsArchive { get; set; }
    }
}