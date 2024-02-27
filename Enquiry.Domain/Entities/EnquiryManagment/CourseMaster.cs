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
    public class CourseMaster
    {
        [Key]
        public int CourseId { get; set; }
        public string Course { get; set; }
        public double Fees { get; set; }
        public string Duration { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [ForeignKey("CreatedBy")]
        public User User_CreatedBy { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
