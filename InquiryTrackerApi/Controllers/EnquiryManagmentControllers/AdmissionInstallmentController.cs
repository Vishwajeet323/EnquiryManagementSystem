using AutoMapper;
using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Interfaces.Generic;
using Enquiry.Domain.Model.EnquiryManagment;
using InquiryTrackerApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InquiryTrackerApi.Controllers.EnquiryManagmentControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdmissionInstallmentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<AdmissionInstallmentController> logger;
        public AdmissionInstallmentController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AdmissionInstallmentController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAdmissionInstallment()
        {
            logger.LogInformation("Inside GetAllAdmissionInstallment method of AdmissionInstallmentController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.AdmissionInstallment.GetAllAsync();
            if (res == null)
            {
                logger.LogInformation("AdmissionInstallment not Found");

                apiResponse.Data = res;
                apiResponse.Message = "AdmissionInstallment not Found";
                apiResponse.Succeeded = false; 
                return Ok(apiResponse);
            }

            logger.LogInformation("AdmissionInstallment Found");

            apiResponse.Data = mapper.Map<List<ShowAdmissionInstallmentDto>>(res);
            apiResponse.Succeeded = true;
            apiResponse.Message = "AdmissionInstallment Found";
            
            return Ok(apiResponse);
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmissionInstallment(AddAdmissionInstallmentDto addAdmissionInstallmentDto)
        {
            logger.LogInformation("Inside AddAdmissionInstallment method of AdmissionInstallmentController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            var admissionInstallmentDomain = mapper.Map<AdmissionInstallment>(addAdmissionInstallmentDto);
            admissionInstallmentDomain.Receiver=Convert.ToInt32(User.Claims.ToList()[0].Value);
            var res = await unitOfWork.AdmissionInstallment.AddAsync(admissionInstallmentDomain);
            if (!res)
            {
                logger.LogInformation("Something went wrong while adding AddAdmissionInstallment");

                apiResponse.Data = StatusCodes.Status406NotAcceptable;
                apiResponse.Message = "Something went wrong";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            apiResponse.Data = StatusCodes.Status201Created;
            apiResponse.Message = "AddAdmissionInstallment added successfully";
            apiResponse.Succeeded = true;

            logger.LogInformation("AddAdmissionInstallment added successfully");
            return Ok(apiResponse);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByIdAdmissionInstallment([FromRoute] int id)
        {
            logger.LogInformation("Inside GetByIdAdmissionInstallment method of AdmissionInstallmentController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.AdmissionInstallment.GetByIdAsync(id);
            if (res == null)
            {
                logger.LogInformation("AdmissionInstallment Not Found");

                apiResponse.Data = res;
                apiResponse.Message = "AdmissionInstallment not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            logger.LogInformation("AdmissionInstallment Found");

            apiResponse.Data = res;
            apiResponse.Message = "AdmissionInstallment Found";
            apiResponse.Succeeded = true;
            return Ok(apiResponse);
            
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAdmissionInstallment([FromRoute] int id)
        {
            logger.LogInformation("Inside DeleteAdmissionInstallment method of AdmissionInstallmentController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.AdmissionInstallment.PermanentDeleteAsync(id);
            if (res)
            {
                apiResponse.Data = StatusCodes.Status406NotAcceptable;
                apiResponse.Message = "AdmissionInstallment Deleted";
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
        public async Task<IActionResult> UpdateAdmissionInstallment([FromRoute] int id, [FromBody] AddAdmissionInstallmentDto addAdmissionInstallmentDto)
        {
            logger.LogInformation("Inside UpdateAdmissionInstallment method of AdmissionInstallmentController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var admissionInstallment = mapper.Map<AdmissionInstallment>(addAdmissionInstallmentDto);
            var res = await unitOfWork.AdmissionInstallment.UpdateAsync(id, admissionInstallment);
            if (res)
            {
                logger.LogInformation("AdmissionInstallment updated successfully");
                return Accepted(new ApiResponse<object>
                {
                    Data = StatusCodes.Status202Accepted,
                    Message = "AdmissionInstallment updated successfully",
                    Succeeded = true
                });
            }
            apiResponse.Data = StatusCodes.Status406NotAcceptable;
            apiResponse.Message = "Something went wrong";
            apiResponse.Succeeded = false;

            logger.LogInformation("Something went wrong while updating AdmissionInstallment");
            return Ok(apiResponse);
        }
    }
}
