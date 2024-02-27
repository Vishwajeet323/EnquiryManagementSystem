using AutoMapper;
using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Interfaces.Generic;
using Enquiry.Domain.Model.EnquiryManagment;
using InquiryTrackerApi.Controllers.Generic;
using InquiryTrackerApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InquiryTrackerApi.Controllers.EnquiryManagmentControllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmissionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<AdmissionController> logger;
        public AdmissionController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AdmissionController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAdmission()
        {
            logger.LogInformation("Inside GetAllAdmission method of AdmissionController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res=await unitOfWork.Admission.GetAllAsync();
            if (res == null)
            {
                logger.LogInformation("Admission not Found");

                apiResponse.Data = res;
                apiResponse.Message = "Admission not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
               
            }
            logger.LogInformation("Admission Found");

            apiResponse.Data = mapper.Map<List<ShowAdmissionDto>>(res);
            apiResponse.Message = "Admission Found";
            apiResponse.Succeeded = true;
            return Ok(apiResponse);
        }
        [HttpPost]
        public async Task<IActionResult>AddAdmission(AddAdmissionDto addAdmission)
        {
            logger.LogInformation("Inside AddAdmission method of AdmissionController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            if (addAdmission == null)
            {
                logger.LogInformation("Please enter data");

                apiResponse.Data = addAdmission;
                apiResponse.Message = "Admission not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            var admissionDomain = mapper.Map<Admission>(addAdmission);
            //user id
            admissionDomain.AdmissionBy=Convert.ToInt32(User.Claims.ToList()[0].Value);
            admissionDomain.AdmissionDate=DateTime.Now;
            var res=await unitOfWork.Admission.AddAsync(admissionDomain);
            if (res == null)
            {
                logger.LogInformation("Something went wrong while adding Admission");

                apiResponse.Data = addAdmission;
                apiResponse.Message = "Something went wrong";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
                
            }
            apiResponse.Data = StatusCodes.Status201Created;
            apiResponse.Message = "Admission added successfully";
            apiResponse.Succeeded = true;

            logger.LogInformation("Admission added successfully");
            return Ok(apiResponse);
            

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult>GetByIdAdmission([FromRoute]int id)
        {
            logger.LogInformation("Inside GetByIdAdmission method of AdmissionController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res=await unitOfWork.Admission.GetByIdAsync(id);
            if (res == null)
            {
                logger.LogInformation("Admission not Found");

                apiResponse.Data = res;
                apiResponse.Message = "Admission not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            logger.LogInformation("Admission Found");

            apiResponse.Data = res;
            apiResponse.Message = "Admission Found";
            apiResponse.Succeeded = true;
            return Ok(apiResponse);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult>DeleteAdmission([FromRoute]int id)
        {
            logger.LogInformation("Inside DeleteAdmission method of AdmissionController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res=await unitOfWork.Admission.PermanentDeleteAsync(id);
            if(res)
            {
                apiResponse.Data = StatusCodes.Status406NotAcceptable;
                apiResponse.Message = "Admission Deleted";
                apiResponse.Succeeded = true;
                return Ok(apiResponse);
            }
            apiResponse.Data = StatusCodes.Status406NotAcceptable;
            apiResponse.Message = "Something went wrong";
            apiResponse.Succeeded = false;
            return Ok(apiResponse);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateAdmission([FromRoute] int id, [FromBody]AddAdmissionDto addAdmissionDto)
        {
            logger.LogInformation("Inside UpdateAdmission method of AdmissionController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var addmission = mapper.Map<Admission>(addAdmissionDto);
            var res = await unitOfWork.Admission.UpdateAsync(id, addmission);
            if(res)
            {
                logger.LogInformation("Admission updated successfully");
                return Accepted(new ApiResponse<object>
                {
                    Data = StatusCodes.Status202Accepted,
                    Message = "Admission updated successfully",
                    Succeeded = true
                });
            }
            apiResponse.Data = StatusCodes.Status406NotAcceptable;
            apiResponse.Message = "Something went wrong";
            apiResponse.Succeeded = false;

            logger.LogInformation("Something went wrong while updating Admission");
            return Ok(apiResponse);
        }

    }
}
