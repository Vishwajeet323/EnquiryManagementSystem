using AutoMapper;
using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Model.EnquiryManagment;
using Enquiry.Domain.Model.Login;

namespace InquiryTrackerApi.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //login
            CreateMap<User,AddUserDto>().ForMember(x=>x.Password,opt=>opt.MapFrom(x=>x.PasswordHash)).ReverseMap();
            CreateMap<User,ShowUserDto>().ReverseMap();
            CreateMap<RoleMaster,AddRoleMasterDto>().ReverseMap();
            CreateMap<RoleMaster,ShowRoleMasterDto>().ReverseMap();


            //Enquiry Managment System
            CreateMap<Admission,AddAdmissionDto>().ReverseMap();
            CreateMap<Admission, ShowAdmissionDto>().ReverseMap();

            // ReferenceMaster
            CreateMap<ReferenceMaster, AddReferenceMasterDto>().ReverseMap();
            CreateMap<ReferenceMaster, ShowReferenceMasterDto>().ReverseMap();

            // StudentEnq
            CreateMap<StudentEnquiry, AddStudentEnquiryDto>().ReverseMap();
            CreateMap<StudentEnquiry, ShowStudentEnquiryDto>().ReverseMap();

            CreateMap<AdmissionInstallment, AddAdmissionInstallmentDto>().ReverseMap();
            CreateMap<AdmissionInstallment, ShowAdmissionInstallmentDto>().ReverseMap();
            
            CreateMap<FollowUp,AddFollowUpDto>().ReverseMap();
            CreateMap<FollowUp,ShowFollowUpDto>().ReverseMap();

            CreateMap<CourseMaster, AddCourseMasterDto>().ReverseMap();
            CreateMap<CourseMaster, ShowCourseMasterDto>().ReverseMap();
        }
    }
}
