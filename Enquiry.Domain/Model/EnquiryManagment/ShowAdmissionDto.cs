using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Entities.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Model.EnquiryManagment
{
    public class ShowAdmissionDto
    {
        public int AdmissionID { get; set; }
        public double TotalFees { get; set; }
        public double? Discount { get; set; }

        [ForeignKey("CourseId")]
        public CourseMaster CourseMaster_CourseId { get; set; }
        public int CourseId { get; set; }

        [ForeignKey("EnquiryId")]
        public StudentEnquiry StudentEnquiry_EnquiryId { get; set; }
        public int EnquiryId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [ForeignKey("AdmissionBy")]
        public User User_AdmissionBy { get; set; }
        public DateTime AdmissionByDate { get; set; }
        public int AdmissionBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
