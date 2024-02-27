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
    public class StudentEnquiryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<StudentEnquiryController> logger;
        public StudentEnquiryController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentEnquiryController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudentEnquiry()
        {
            logger.LogInformation("Inside GetAllStudentEnquiry method of StudentEnquiryController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.StudentEnquiry.GetAllAsync();
            if (res == null)
            {
                logger.LogInformation("StudentEnquiry not Found");

                apiResponse.Data = res;
                apiResponse.Message = "StudentEnquiry not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }

            logger.LogInformation("StudentEnquiry Found");

            apiResponse.Data = mapper.Map<List<ShowStudentEnquiryDto>>(res);
            apiResponse.Succeeded = true;
            apiResponse.Message = "StudentEnquiry Found";

            return Ok(apiResponse);
            

        }
        [HttpPost]
        public async Task<IActionResult> AddStudentEnquiry(AddStudentEnquiryDto addStudentEnquiryDto)
        {
            logger.LogInformation("Inside AddStudentEnquiry method of StudentEnquiryController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            var studentEnquiryDomain = mapper.Map<StudentEnquiry>(addStudentEnquiryDto);
            studentEnquiryDomain.CreatedBy = Convert.ToInt32(User.Claims.ToList()[0].Value);
            studentEnquiryDomain.CreatedDate = DateTime.Now;
            var res = await unitOfWork.StudentEnquiry.AddAsync(studentEnquiryDomain);

            if (!res)
            {
                logger.LogInformation("Something went wrong while adding StudentEnquiry");

                apiResponse.Data = StatusCodes.Status406NotAcceptable;
                apiResponse.Message = "Something went wrong";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            apiResponse.Data = StatusCodes.Status201Created;
            apiResponse.Message = "StudentEnquiry added successfully";
            apiResponse.Succeeded = true;

            logger.LogInformation("StudentEnquiry added successfully");
            return Ok(apiResponse);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByIdStudentEnquiry([FromRoute] int id)
        {
            logger.LogInformation("Inside GetByIdStudentEnquiry method of StudentEnquiryController");
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            var res = await unitOfWork.StudentEnquiry.GetByIdAsync(id);
            if (res == null)
            {
                logger.LogInformation("StudentEnquiry Not Found");

                apiResponse.Data = res;
                apiResponse.Message = "StudentEnquiry not Found";
                apiResponse.Succeeded = false;
                return Ok(apiResponse);
            }
            logger.LogInformation("StudentEnquiry Found");

            apiResponse.Data = res;
            apiResponse.Message = "StudentEnquiry Found";
            apiResponse.Succeeded = true;
            return Ok(apiResponse);
           
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStudentEnquiry([FromRoute] int id)
        {
            return Ok("Task Remaining");
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStudentEnquiry([FromRoute] int id, [FromBody] AddStudentEnquiryDto addStudentEnquiryDto)
        {
            return Ok("Task Remaining");
        }
    }
}
