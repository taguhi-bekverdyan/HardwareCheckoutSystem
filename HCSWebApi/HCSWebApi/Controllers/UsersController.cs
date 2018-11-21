using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCSWebApi.Models;
using HCSWebApi.Models.Context;
using HCSWebApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardwareCheckoutSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {

        private readonly UserService _service;

        #region main
        public UsersController(DataContext context)
        {
            _service = new UserService(context);

        }
        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<User> users = await _service.FindAll();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{guid}", Name = "GetUser")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid guid)
        {
            try
            {
                User user = await _service.FindById(guid);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        //[HttpGet("byName/{name}", Name = "GetRequestByName")]
        //public async Task<IActionResult> GetCategoryById([FromRoute]string name)
        //{
        //    try
        //    {
        //        Category category = await _service.FindByName(name);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(category);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e);
        //    }
        //}


        #endregion

        #region POST
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]User user)
        {
            try
            {
                await _service.Insert(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT
        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute]Guid guid, [FromBody]User user)
        {
            try
            {
                User result = await _service.FindById(guid);
                if (result == null)
                {
                    return NotFound();
                }
                result.Id = user.Id;
                await _service.Update(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region DELETE
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute]Guid guid)
        {
            try
            {
                User user = await _service.FindById(guid);
                if (user == null)
                {
                    return NotFound();
                }
                await _service.Delete(user);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion
    }
}
