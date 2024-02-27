using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Interfaces.Login
{
    public interface IRoleMaster:IGenericRepository<RoleMaster>
    {
        Task<bool>UpdateRoleAsync(int roleId, RoleMaster role);
        Task<bool>DeleteRoleAsync(int id);
    }
}
