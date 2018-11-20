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
  public class DevicesController : ControllerBase
  {
    private DeviceService _service { get; set; }

    public DevicesController(DataContext context)
    {
      _service = new DeviceService(context);      
    }

    // GET: api/Devices
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var devices = await _service.FindAll();
        if (devices != null)
        {
          return Ok(devices);
        }
        else return NotFound(devices);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // GET: api/Devices/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      try
      {
        var devices = await _service.FindById(id);
        if (devices != null)
        {
          return Ok(devices);
        }
        else return NotFound(devices);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // GET: api/Devices/5
    [HttpGet("by-sn/{sn}")]
    public async Task<IActionResult> GetBySn(string sn)
    {
      try
      {
        var devices = await _service.FindBySn(sn);
        if (devices != null)
        {
          return Ok(devices);
        }
        else return NotFound(devices);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // POST: api/Devices
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Device device)
    {
      try
      {
        await _service.Insert(device);
        return Created(device.Id.ToString(), device);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // PUT: api/Devices/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Device device)
    {
      try
      {
        await _service.Update(device);
        return Ok(device);
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
