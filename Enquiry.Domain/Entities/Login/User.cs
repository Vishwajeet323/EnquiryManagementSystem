using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Entities.Login
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? MiddleName { get; set; }

        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
      
        public DateTime? ModifiedDate { get; set; }
      
        public int RoleId { get; set; }

        //nav
       /* [ForeignKey(nameof(RoleId))]
        public RoleMaster RoleMaster { get; set; }*/
    }
}
