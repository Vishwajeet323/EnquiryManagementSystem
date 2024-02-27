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
    public class AddAdmissionDto
    {

        public double TotalFees { get; set; }
        public double? Discount { get; set; }
        public int CourseId { get; set; }

        public int EnquiryId { get; set; }
       // public DateTime? ModifiedDate { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime AdmissionDate { get; set; }
        //public int AdmissionBy { get; set; }
    }
}
