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
    public class FollowUpController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<FollowUpController> logger;
        public FollowUpController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FollowUpController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFollowUp()
        {
            logger.LogInformation("Inside GetAllFollowUp method of FollowUpController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.FollowUp.GetAllAsync();
            if (res == null)
            {
                logger.LogInformation("FollowUp not Found");

                apiResponse.Data = res;
                apiResponse.Message = "FollowUp not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }

            logger.LogInformation("FollowUp Found");

            apiResponse.Data = mapper.Map<List<ShowFollowUpDto>>(res);
            apiResponse.Succeeded = true;
            apiResponse.Message = "FollowUp Found";

            return Ok(apiResponse);
  

        }
        [HttpPost]
        public async Task<IActionResult> AddFollowUp(AddFollowUpDto addFollowUp)
        {
            logger.LogInformation("Inside AddFollowUp method of FollowUpController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            var followUpDomain = mapper.Map<FollowUp>(addFollowUp);
            followUpDomain.FollowUpBy = Convert.ToInt32(User.Claims.ToList()[0].Value);
            followUpDomain.CurrentFollowUpDate = DateTime.Now;
            var res = await unitOfWork.FollowUp.AddAsync(followUpDomain);
            
            if (!res)
            {
                logger.LogInformation("Something went wrong while adding Followup");

                apiResponse.Data = StatusCodes.Status406NotAcceptable;
                apiResponse.Message = "Something went wrong";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            apiResponse.Data = StatusCodes.Status201Created;
            apiResponse.Message = "Followup added successfully";
            apiResponse.Succeeded = true;

            logger.LogInformation("Followup added successfully");
            return Ok(apiResponse);


        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByIdFollowUp([FromRoute] int id)
        {
            logger.LogInformation("Inside GetByIdFollowUp method of FollowUpController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.FollowUp.GetByIdAsync(id);
            if (res == null)
            {
                logger.LogInformation("FollowUp Not Found");

                apiResponse.Data = res;
                apiResponse.Message = "FollowUp not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            logger.LogInformation("FollowUp Found");

            apiResponse.Data = res;
            apiResponse.Message = "FollowUp Found";
            apiResponse.Succeeded = true;
            return Ok(apiResponse);  
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteFollowUp([FromRoute] int id)
        {
            logger.LogInformation("Inside DeleteFollowUp method of FollowUpController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.FollowUp.PermanentDeleteAsync(id);
            if (res)
            {
                apiResponse.Data = StatusCodes.Status406NotAcceptable;
                apiResponse.Message = "FollowUp Deleted";
                apiResponse.Succeeded = true;
                return Ok(apiResponse);
            }
            apiResponse.Data = StatusCodes.Status406NotAcceptable;
            apiResponse.Message = "Something went wrong while deleting FollowUp";
            apiResponse.Succeeded = false;
            return Ok(apiResponse);
            
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateFollowUp([FromRoute] int id, [FromBody] AddFollowUpDto addFollowUp)
        {
            logger.LogInformation("Inside UpdateFollowUp method of FollowUpController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var Followup = mapper.Map<FollowUp>(addFollowUp);
            var res = await unitOfWork.FollowUp.UpdateAsync(id, Followup);
            if (res)
            {
                logger.LogInformation("Followup updated successfully");
                return Accepted(new ApiResponse<object>
                {
                    Data = StatusCodes.Status202Accepted,
                    Message = "Followup updated successfully",
                    Succeeded = true
                });
            }
            apiResponse.Data = StatusCodes.Status406NotAcceptable;
            apiResponse.Message = "Something went wrong";
            apiResponse.Succeeded = false;
            logger.LogInformation("Something went wrong while updating Followup");
            return Ok(apiResponse);
        }
    }
}
