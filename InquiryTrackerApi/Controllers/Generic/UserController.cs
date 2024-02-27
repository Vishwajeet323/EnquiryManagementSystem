using AutoMapper;
using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.Generic;
using Enquiry.Domain.Model.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace InquiryTrackerApi.Controllers.Generic
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;

        public UserController(IUnitOfWork unitOfWork,IMapper mapper,ILogger<UserController> logger)
        {
            this.unitOfWork=unitOfWork;
            this.mapper=mapper;
            this.logger=logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            logger.LogInformation("GetAllUsers Action methode is Invoked");
            var users = await unitOfWork.User.GetAllAsync();
            
            var ActiveUsers=new List<User>();
            foreach(var user in users)
            {
                if(!user.IsDeleted)
                {
                   ActiveUsers.Add(user);
                }
            }
           //maping
            var userDto = mapper.Map<List<ShowUserDto>>(ActiveUsers);
            logger.LogInformation($"Finished GetAllUsers methode with data: {JsonSerializer.Serialize(userDto)}");
            return Ok(userDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            logger.LogInformation("GetUserById Action methode is Invoked");
            var user = await unitOfWork.User.GetByIdAsync(id);
            if (user == null) { return NotFound(); }
            var userDto = mapper.Map<ShowUserDto>(user);
            logger.LogInformation($"finished GetById request with data: {JsonSerializer.Serialize(userDto)}");
            return Ok(userDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto userDto)
        {
            logger.LogInformation("AddUser Action methode is Invoked");
            if (userDto == null)
            {
                return BadRequest();
            }
           // Random random = new Random();

         
            userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password, 10);
            var userDomain = mapper.Map<User>(userDto);
            var res=await unitOfWork.User.AddUserAsync(userDomain);
            if(res==false)
            {
                return BadRequest("username alredy exist");
            }
            //mapp
            var showUserDto=mapper.Map<ShowUserDto>(userDomain);
            logger.LogInformation($"finished GetById request with data: {JsonSerializer.Serialize(showUserDto)}");
            return Ok("User Created Successfully");
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute]int id)
        {
            logger.LogInformation("DeleteUser action methode is invoked");
            var res=await unitOfWork.User.DeleteUserAsync(id);
            if(res)
            {
                unitOfWork.Complete();
                return Ok("User Deleted Successfully");
            }
            return NotFound();
        }
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUserPartially([FromRoute]int id,[FromBody]JsonPatchDocument<AddUserDto> userDto)
        {
            logger.LogInformation("UpdateUserPartially action methode is invoked");
            var user = await unitOfWork.User.GetByIdAsync(id);
            if(user == null)
            {
                return BadRequest();
            }
            userDto.ApplyTo(mapper.Map<AddUserDto>(user));
            user.ModifiedDate = DateTime.UtcNow;
            await unitOfWork.Complete();
            return Ok("User updated successfully");

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] AddUserDto userDto)
        {
            logger.LogInformation("UpdateUser action methode is invoked");
            var user = await unitOfWork.User.UpdateUserAsync(id, mapper.Map<User>(userDto));

            if (!user) { return BadRequest(); }

            await unitOfWork.Complete();
            return Ok("User Updated");
        }

    } 
}
