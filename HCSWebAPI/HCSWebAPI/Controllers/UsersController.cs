using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCSWebAPI.Models;
using HCSWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HCSWebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private UserService _service { get; set; }

    public UsersController(DataContext context)
    {
      _service = new UserService(context);
    }

    // GET: api/Users
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var users = await _service.FindAll();
        if (users != null)
        {
          return Ok(users);
        }
        else return NotFound(users);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      try
      {
        var users = await _service.FindById(id);
        if (users != null)
        {
          return Ok(users);
        }
        else return NotFound(users);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // POST: api/Users
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
      try
      {
        await _service.Insert(user);
        return Created(user.Id.ToString(), user);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] User user)
    {
      try
      {
        await _service.Update(user);
        return Ok(user);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      try
      {
        await _service.Delete(id);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }
  }
}
