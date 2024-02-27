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
    public class ReferenceMasterController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<ReferenceMasterController> logger;
        public ReferenceMasterController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReferenceMasterController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReferenceMaster()
        {
            logger.LogInformation("Inside GetAllReferenceMaster method of ReferenceMasterController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.ReferenceMaster.GetAllAsync();
            if (res == null)
            {
                logger.LogInformation("ReferenceMaster not Found");

                apiResponse.Data = res;
                apiResponse.Message = "ReferenceMaster not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }

            logger.LogInformation("ReferenceMaster Found");

            apiResponse.Data = mapper.Map<List<ShowReferenceMasterDto>>(res);
            apiResponse.Succeeded = true;
            apiResponse.Message = "ReferenceMaster Found";

            return Ok(apiResponse);
           
        }
        [HttpPost]
        public async Task<IActionResult> AddReferenceMaster(AddReferenceMasterDto addReferenceMaster)
        {
            logger.LogInformation("Inside AddReferenceMaster method of ReferenceMasterController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            var referenceMasterDomain = mapper.Map<ReferenceMaster>(addReferenceMaster);
            referenceMasterDomain.CreatedBy = Convert.ToInt32(User.Claims.ToList()[0].Value);
            referenceMasterDomain.CreatedDate = DateTime.Now;
            var res = await unitOfWork.ReferenceMaster.AddAsync(referenceMasterDomain);

            if (!res)
            {
                logger.LogInformation("Something went wrong while adding ReferenceMaster");

                apiResponse.Data = StatusCodes.Status406NotAcceptable;
                apiResponse.Message = "Something went wrong";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            apiResponse.Data = StatusCodes.Status201Created;
            apiResponse.Message = "ReferenceMaster added successfully";
            apiResponse.Succeeded = true;

            logger.LogInformation("ReferenceMaster added successfully");
            return Ok(apiResponse);
           
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByIdReferenceMaster([FromRoute] int id)
        {
            logger.LogInformation("Inside GetByIdReferenceMaster method of ReferenceMasterController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.ReferenceMaster.GetByIdAsync(id);
            if (res == null)
            {
                logger.LogInformation("ReferenceMaster Not Found");

                apiResponse.Data = res;
                apiResponse.Message = "ReferenceMaster not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            logger.LogInformation("ReferenceMaster Found");

            apiResponse.Data = res;
            apiResponse.Message = "ReferenceMaster Found";
            apiResponse.Succeeded = true;
            return Ok(apiResponse);
           
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteReferenceMaster([FromRoute] int id)
        {
            return Ok("Task Remaining");
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateReferenceMaster([FromRoute] int id, [FromBody] AddReferenceMasterDto addReferenceMaster)
        {
            return Ok("Task Remaining");
        }
    }
}
