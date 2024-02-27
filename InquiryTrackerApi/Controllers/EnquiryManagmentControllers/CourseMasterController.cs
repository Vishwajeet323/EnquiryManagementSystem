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
    public class CourseMasterController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<CourseMasterController> logger;
        public CourseMasterController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CourseMasterController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCourseMaster()
        {
            logger.LogInformation("Inside GetAllCourseMaster method of CourseMasterController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.CourseMaster.GetAllAsync();
            if (res == null)
            {
                logger.LogInformation("CourseMaster not Found");

                apiResponse.Data = res;
                apiResponse.Message = "CourseMaster not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);

            }
            logger.LogInformation("CourseMaster Found");

            apiResponse.Data = mapper.Map<List<ShowCourseMasterDto>>(res);
            apiResponse.Message = "CourseMaster Found";
            apiResponse.Succeeded = true;
            return Ok(apiResponse);
           
        }
        [HttpPost]
        public async Task<IActionResult> AddCourseMaster(AddCourseMasterDto addCourseMaster)
        {
           logger.LogInformation("Inside AddCourseMaster method of CourseMasterController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            if (addCourseMaster == null)
            {
                logger.LogInformation("Please enter data");

                apiResponse.Data = addCourseMaster;
                apiResponse.Message = "CourseMaster not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            var courseMasterDomain = mapper.Map<CourseMaster>(addCourseMaster);
            
            courseMasterDomain.CreatedBy = Convert.ToInt32(User.Claims.ToList()[0].Value);
            var res = await unitOfWork.CourseMaster.AddAsync(courseMasterDomain);
            if (res == null)
            {
                logger.LogInformation("Something went wrong while adding CourseMaster");

                apiResponse.Data = res;
                apiResponse.Message = "Something went wrong";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);

            }
            apiResponse.Data = StatusCodes.Status201Created;
            apiResponse.Message = "CourseMaster added successfully";
            apiResponse.Succeeded = true;

            logger.LogInformation("CourseMaster added successfully");
            return Ok(apiResponse);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByIdCourseMaster([FromRoute] int id)
        {
            logger.LogInformation("Inside GetByIdCourseMaster method of CourseMasterController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.CourseMaster.GetByIdAsync(id);
            if (res == null)
            {
                logger.LogInformation("CourseMaster Not Found");

                apiResponse.Data = res;
                apiResponse.Message = "CourseMaster not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            logger.LogInformation("CourseMaster Found");

            apiResponse.Data = res;
            apiResponse.Message = "CourseMaster Found";
            apiResponse.Succeeded = true;
            return Ok(apiResponse);
           
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCourseMaster([FromRoute] int id)
        {
            
            logger.LogInformation("Inside DeleteCourseMaster method of CourseMasterController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.CourseMaster.PermanentDeleteAsync(id);
            if (res)
            {
                apiResponse.Data = StatusCodes.Status406NotAcceptable;
                apiResponse.Message = "CourseMaster Deleted";
                apiResponse.Succeeded = true;
                return Ok(apiResponse);
            }
            apiResponse.Data = StatusCodes.Status406NotAcceptable;
            apiResponse.Message = "Something went wrong while deletin CourseMaster";
            apiResponse.Succeeded = false;
            return Ok(apiResponse);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCourseMaster([FromRoute] int id, [FromBody] AddCourseMasterDto addCourseMaster)
        {
            logger.LogInformation("Inside UpdateCourseMaster method of CourseMasterController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var CourseMaster = mapper.Map<CourseMaster>(addCourseMaster);
            var res = await unitOfWork.CourseMaster.UpdateAsync(id, CourseMaster);
            if (res)
            {
                logger.LogInformation("CourseMaster updated successfully");
                return Accepted(new ApiResponse<object>
                {
                    Data = StatusCodes.Status202Accepted,
                    Message = "CourseMaster updated successfully",
                    Succeeded = true
                });
            }
            apiResponse.Data = StatusCodes.Status406NotAcceptable;
            apiResponse.Message = "Something went wrong";
            apiResponse.Succeeded = false;
            logger.LogInformation("Something went wrong while updating CourseMaster");
            return Ok(apiResponse);
        }
    }
}
