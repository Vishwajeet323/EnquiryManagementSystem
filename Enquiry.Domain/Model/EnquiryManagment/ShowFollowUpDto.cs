using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Entities.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Model.EnquiryManagment
{
    public class ShowFollowUpDto
    {
        public int FollowUpId { get; set; }
        public string Comments { get; set; }
        public DateTime CurrentFollowUpDate { get; set; }
        public DateTime? NextFolloUpDate { get; set; }
        public int? ModifiedBy { get; set; }//UserId
        public DateTime? ModifiedDate { get; set; }
        public bool IsFollowUp { get; set; }
        [ForeignKey("EnquiryId")]
        public StudentEnquiry StudentEnquiry_EnquiryId { get; set; }

        public int EnquiryId { get; set; }
        [ForeignKey("FollowUpBy")]
        public User User_FollowUpBy { get; set; }
        public int FollowUpBy { get; set; }
    }
}
