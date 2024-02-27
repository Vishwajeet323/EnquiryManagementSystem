using Enquiry.DataAccess.Context;
using Enquiry.DataAccess.Implementation.Generic;
using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.Generic;
using Enquiry.Domain.Interfaces.Login;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.DataAccess.Implementation.Login 
{
    public class RoleMasterRepository : GenericRepository<RoleMaster>,IRoleMaster
    {
        private readonly EnquiryDbContext dbContext;
        private readonly ILogger logger;
        public RoleMasterRepository(EnquiryDbContext dbContext,ILogger logger) : base(dbContext, logger)
        {
            this.dbContext=dbContext;
            this.logger = logger;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            logger.LogInformation("DeleteRoleAsync Methode Is invoked");
            var roleMaster=await dbContext.RoleMasters.FindAsync(id);
            if (roleMaster == null) { return false; }

            roleMaster.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRoleAsync(int roleId, RoleMaster role)
        {
            logger.LogInformation("UpdateRoleAsync Methode Is invoked");
            var roleMaster = await dbContext.RoleMasters.FindAsync(roleId);
            if (roleMaster == null) { return false; }

            roleMaster.Role = role.Role;
            roleMaster.ModifiedDate = DateTime.Now;
            roleMaster.ModifiedBy = role.ModifiedBy;

            await dbContext.SaveChangesAsync();
            return true;

        }
    }
}
