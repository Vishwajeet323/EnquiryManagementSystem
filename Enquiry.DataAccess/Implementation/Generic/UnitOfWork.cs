using Enquiry.DataAccess.Context;
using Enquiry.DataAccess.Implementation.EnquiryManagment;
using Enquiry.DataAccess.Implementation.Login;
using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.EnquiryManagment;
using Enquiry.Domain.Interfaces.Generic;
using Enquiry.Domain.Interfaces.Login;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.DataAccess.Implementation.Generic
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnquiryDbContext _dbContext;
        private readonly ILogger _logger;

        public IUserRepo<User> User { get; private set; }

        public IRoleMaster RoleMaster { get; private set; }

        public IAdmissionRepository Admission { get; private set; }

        public IFollowUpRepository FollowUp { get; private set; }

        public IAdmissionInstallmentRepository AdmissionInstallment { get; private set; }

        public ICourseMasterRepository CourseMaster { get; private set; }

        public IReferenceMasterRepository ReferenceMaster { get; private set; }

        public IStudentEnquiryRepository StudentEnquiry { get; private set; }

        public UnitOfWork(EnquiryDbContext dbContext,ILogger<UnitOfWork> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            User = new UserRepository(_dbContext,_logger);
            RoleMaster = new RoleMasterRepository(_dbContext,_logger);
            Admission=new AdmissionRepository(_dbContext,_logger);
            AdmissionInstallment=new AdmissionInstallmentRepository(_dbContext,_logger);
            CourseMaster=new CourseMasterRepository(_dbContext,_logger);
            FollowUp=new FollowUpRepository(_dbContext,_logger);
            ReferenceMaster=new ReferenceMasterRepository(_dbContext,_logger);
            StudentEnquiry=new StudentEnquiryRepository(_dbContext,_logger);

        }

        public Task Complete()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
