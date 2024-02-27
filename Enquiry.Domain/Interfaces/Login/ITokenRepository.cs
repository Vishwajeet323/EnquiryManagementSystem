using Enquiry.Domain.Entities.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Interfaces.Login
{
    public interface ITokenRepository
    {
        string CreateJwtToken(User user);
    }
}
