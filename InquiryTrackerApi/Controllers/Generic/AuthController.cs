using AutoMapper;
using Enquiry.DataAccess.Implementation.Login;
using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.Generic;
using Enquiry.Domain.Interfaces.Login;
using Enquiry.Domain.Model.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InquiryTrackerApi.Controllers.Generic
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AuthController> logger;
        private readonly ITokenRepository tokenRepository;
        private readonly IMapper mapper;

        public AuthController(IMapper mapper,IUnitOfWork unitOfWork,ILogger<AuthController> logger,ITokenRepository tokenRepository)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.tokenRepository = tokenRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginInfo)
        {
            logger.LogInformation("Login action methode is invoked");
            if (loginInfo == null) { return BadRequest(); }

            var user=await unitOfWork.User.GetUserByUserNameAsync(loginInfo.UserName);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            var pass = BCrypt.Net.BCrypt.Verify(loginInfo.Password, user.PasswordHash);
            
            if (!pass) return BadRequest("Wrong Password");

            var jwtToken = tokenRepository.CreateJwtToken(user);

            var response = new LoginResponse
            {
                JwtToken = jwtToken
            };
            logger.LogInformation($"Finished Login methode with data: {response}");
            return Ok(response);
        }
    }
}
