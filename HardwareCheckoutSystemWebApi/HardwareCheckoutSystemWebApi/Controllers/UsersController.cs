using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareCheckoutSystemWebApi.Context.Models;
using HardwareCheckoutSystemWebApi.Models;
using HardwareCheckoutSystemWebApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardwareCheckoutSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserService _service;

        #region Ctor

        public UsersController(DataContext context)
        {
            _service = new UserService(context);
        }

        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.FindAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid guid)
        {
            try
            {
                User user = await _service.FindUserById(guid);
                if (user == null) { return NotFound(); }
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]User user)
        {
            try
            {
                await _service.Insert(user);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]User user)
        {
            try
            {
                if (await _service.Update(user))
                {
                    return Ok(user);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region DELETE
        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid guid)
        {
            try
            {
                await _service.DeleteUserById(guid);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion
    }
}
