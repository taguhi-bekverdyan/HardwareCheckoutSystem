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
  public class RequestsController : ControllerBase
  {
    private RequestService _service { get; set; }

    public RequestsController(DataContext context)
    {
      _service = new RequestService(context);
    }

    // GET: api/Requests
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var requests = await _service.FindAll();
        if (requests != null)
        {
          return Ok(requests);
        }
        else return NotFound(requests);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // GET: api/Requests/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      try
      {
        var requests = await _service.FindById(id);
        if (requests != null)
        {
          return Ok(requests);
        }
        else return NotFound(requests);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // POST: api/Requests
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Request request)
    {
      try
      {
        await _service.Insert(request);
        return Created(request.Id.ToString(), request);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // PUT: api/Requests/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] Request request)
    {
      try
      {
        await _service.Update(request);
        return Ok(request);
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
