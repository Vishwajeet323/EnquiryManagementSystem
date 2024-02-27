using Enquiry.Domain.Entities.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Entities.EnquiryManagment
{
    public class AdmissionInstallment
    {
        [Key]
        public int AdmissionInstallmentId { get; set; }
        public double PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
        public double DueAmount { get; set; }
        public double Inastallment { get; set; }
        public DateTime InastallmentDate { get; set; }
        
        [ForeignKey("AdmissionId")]
        public Admission Admission_AdmissionId { get; set; }
        public int AdmissionId { get; set; }
        [ForeignKey("Receiver")]
        public User User_Receiver { get; set; }
        public int Receiver { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        //nav
       /* [ForeignKey(nameof(CreatedBy))]
        public User User { get; set; }
        [ForeignKey(nameof(AdmissionId))]
        public Admission Admission { get; set; }*/
    }
}
