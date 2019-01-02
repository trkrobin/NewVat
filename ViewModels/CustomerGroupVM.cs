using System;

namespace newvat.ViewModels
{
    public class CustomerGroupVM
    {
        public string CustomerGroupID { get; set; }
        public string CustomerGroupName { get; set; }
        public string CustomerGroupDescription { get; set; }
        public string GroupType { get; set; }
        public string Comments { get; set; }
        public bool ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string Operation { get; set; }
    }
}