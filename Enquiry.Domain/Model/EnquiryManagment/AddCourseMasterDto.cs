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
    public class AddCourseMasterDto
    {
       
        public string Course { get; set; }
        public double Fees { get; set; }
        public string Duration { get; set; }
        
       /* public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }*/
        
    }
}
