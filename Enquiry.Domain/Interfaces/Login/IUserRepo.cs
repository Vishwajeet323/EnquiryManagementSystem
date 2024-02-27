using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Interfaces.Login
{
    public interface IUserRepo<T>:IGenericRepository<User>
    {
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUserAsync(int id,User user);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<bool> AddUserAsync(User user);


    }
}
