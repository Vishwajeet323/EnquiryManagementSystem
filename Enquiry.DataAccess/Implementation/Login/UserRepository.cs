using Enquiry.DataAccess.Context;
using Enquiry.DataAccess.Implementation.Generic;
using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.DataAccess.Implementation.Login
{
    public class UserRepository : GenericRepository<User>,IUserRepo<User>
    {
        private readonly EnquiryDbContext dbContext;
        private readonly ILogger logger;
        public UserRepository(EnquiryDbContext dbContext,ILogger logger) : base(dbContext, logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user=await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            user.IsDeleted = true;
            return true;
        }

        

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = await dbContext.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            
            return user;
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            var existingUser= await dbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return false;
            }
            if (existingUser.IsDeleted) { return false; }
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.UserName = user.UserName;
            existingUser.Email= user.Email;
            existingUser.ModifiedDate= DateTime.Now;
            existingUser.RoleId= user.RoleId;
            await dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                logger.LogInformation("Inside AddAsync method of GenericRepository");
                var isExisting= await dbContext.Users.Where(x=>x.UserName == user.UserName).FirstOrDefaultAsync();
                if (isExisting != null)
                {
                    return false;
                }
                Console.WriteLine(isExisting + "hey");
                var res = await dbContext.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occurred in AddAsync method of GenericRepository: {0}", ex.StackTrace);
                throw new Exception(ex.StackTrace);
            }
        }
    }
}
