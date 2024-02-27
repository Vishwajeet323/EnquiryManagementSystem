using AutoMapper;
using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.Generic;
using Enquiry.Domain.Model.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InquiryTrackerApi.Controllers.Generic
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleMasterController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<RoleMasterController> logger;
        private readonly IMapper mapper;

        public RoleMasterController(IUnitOfWork unitOfWork, ILogger<RoleMasterController> logger,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
       // [Authorize]
        public async Task<IActionResult> GetAllRoles()
        {
            
            logger.LogInformation("GetAllRoles action methode is invoked");
            logger.LogWarning("warning from logger");
            var roles = await unitOfWork.RoleMaster.GetAllAsync();
            var activeRoles=new List<RoleMaster>();
            foreach (var role in roles)
            {
                if (!role.IsDeleted)
                {
                    activeRoles.Add(role);
                }
            }
            //map
            var rolesDto = mapper.Map<List<ShowRoleMasterDto>>(activeRoles);
            logger.LogInformation($"Finished GetallRoles methode with data: {JsonSerializer.Serialize(rolesDto)}");

           // throw new Exception("wrong");
            return Ok(rolesDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByIdRole([FromRoute] int id)
        {

            logger.LogInformation("GetByIdRole Action methode is Invoked");
            var role=await unitOfWork.RoleMaster.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            //mapping
            var roleDto = mapper.Map<ShowRoleMasterDto>(role);
            logger.LogInformation($"Finished GetRoleById methode with data: {JsonSerializer.Serialize(roleDto)}");
            return Ok(roleDto);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddRole([FromBody]AddRoleMasterDto roleDto)
        {
            logger.LogInformation("AddRole Action methode is Invoked");
            var role = mapper.Map<RoleMaster>(roleDto);
            //current user id
            role.CreatedBy=Convert.ToInt32(User.Claims.ToList()[0].Value);
            role.CreatedDate= DateTime.Now;
           var isAdded= await unitOfWork.RoleMaster.AddAsync(role);
            if (isAdded)
            {
                await unitOfWork.Complete();
                //map
                return Ok(mapper.Map<ShowRoleMasterDto>(role));
            }
            return BadRequest("Role is Not Added");

        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteRoleMaster([FromRoute]int id)
        {
            logger.LogInformation("DeleteRoleMaster action methode is invoked");
            var res=await unitOfWork.RoleMaster.DeleteRoleAsync(id);
            if (res) { return Ok("User Deleted Successfully"); }
            return BadRequest("Something went wrong");
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateRoleMaster([FromRoute] int id, [FromBody] AddRoleMasterDto roleMasterDto)
        {
            logger.LogInformation("UpdateRoleMaster action methode is invoked");
            var res = await unitOfWork.RoleMaster.UpdateRoleAsync(id, mapper.Map<RoleMaster>(roleMasterDto));
            if (res) { return Ok("Role Master Updated Successfully"); }
            return BadRequest("Something Went Wrong");
        }

        /*[HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateRole([FromRoute] int id, [FromBody] AddRoleMasterDto roleMasterDto)
        {
            logger.LogInformation("UpdateRole Methode is Invoked");
            var role = mapper.Map<RoleMaster>(roleMasterDto);
            role.ModifiedDate = DateTime.Now;
            await Console.Out.WriteLineAsync("hww:" + DateTime.Now);
            var res = await unitOfWork.RoleMaster.UpdateAsync(id, role);
            logger.LogInformation($"Finished GetallRoles methode with data: {JsonSerializer.Serialize(res)}");
            if (res)
            {
                return Ok("Role Master Updated Successfully");
            }
            return BadRequest("Not updated");
        }*/


    }
}

