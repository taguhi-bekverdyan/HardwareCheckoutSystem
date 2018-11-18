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
  public class ResponsesController : ControllerBase
  {
    private ResponseService _service { get; set; }

    public ResponsesController(DataContext context)
    {
      _service = new ResponseService(context);
    }

    // GET: api/Responses
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var responses = await _service.FindAll();
        if (responses != null)
        {
          return Ok(responses);
        }
        else return NotFound(responses);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // GET: api/Responses/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      try
      {
        var responses = await _service.FindById(id);
        if (responses != null)
        {
          return Ok(responses);
        }
        else return NotFound(responses);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // POST: api/Responses
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Response response)
    {
      try
      {
        await _service.Insert(response);
        return Created(response.Id.ToString(), response);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // PUT: api/Responses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] Response response)
    {
      try
      {
        await _service.Update(response);
        return Ok(response);
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
