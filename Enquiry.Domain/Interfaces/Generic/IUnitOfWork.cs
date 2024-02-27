using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.EnquiryManagment;
using Enquiry.Domain.Interfaces.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Interfaces.Generic
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepo<User> User { get; }
        IRoleMaster RoleMaster { get; }
        IAdmissionRepository Admission {  get; }
        IFollowUpRepository FollowUp { get; }
        IAdmissionInstallmentRepository AdmissionInstallment { get; }
        ICourseMasterRepository CourseMaster { get; }
        IReferenceMasterRepository ReferenceMaster { get; }
        IStudentEnquiryRepository StudentEnquiry { get; }


        Task Complete();
    }
}
