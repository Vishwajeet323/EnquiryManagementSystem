using Enquiry.DataAccess.Context;
using Enquiry.DataAccess.Implementation.Generic;
using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Interfaces.EnquiryManagment;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.DataAccess.Implementation.EnquiryManagment
{
    public class AdmissionRepository : GenericRepository<Admission>, IAdmissionRepository
    {
        public readonly EnquiryDbContext dbContext;
        public readonly ILogger logger;
        public AdmissionRepository(EnquiryDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
    }
}
