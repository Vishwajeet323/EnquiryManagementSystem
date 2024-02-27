using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Entities.Login
{
    public class RoleMaster
    {
        [Key]
        public int RoleId { get; set; }
        public string Role { get; set; }
        [ForeignKey("CreatedBy")]
        public User? User_CreatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

    }
}
