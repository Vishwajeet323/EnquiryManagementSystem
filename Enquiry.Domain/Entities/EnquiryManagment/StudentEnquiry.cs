using Enquiry.Domain.Entities.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Entities.EnquiryManagment
{
    public class StudentEnquiry
    {

        [Key]
        public int EnquiryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Collage { get; set; }
        public string Qualification { get; set; }
        public string PassoutYear { get; set; }
        public string WorkExperience { get; set; }
        public string AreaOfInterest { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [ForeignKey("ReferenceId")]
        public ReferenceMaster ReferenceMaster_ReferenceId { get; set; }
        public int ReferenceId { get; set; }

        [ForeignKey("CreatedBy")]       
        public User User_CreatedBy { get; set; }
        public int CreatedBy { get; set; }
        public int? MofdifiedBy { get; set; }
        public bool IsEnquiryStatus { get; set; }
       
    }
}
