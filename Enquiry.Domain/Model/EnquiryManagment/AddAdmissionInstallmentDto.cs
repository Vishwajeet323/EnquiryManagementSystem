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
    public class AddAdmissionInstallmentDto
    {

        public double PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
        public double DueAmount { get; set; }
        public double Inastallment { get; set; }
        public DateTime InastallmentDate { get; set; }
        public int AdmissionId { get; set; }
        //public int Receiver { get; set; }
       // public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }
}
