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

      Seed(context);
    }

    private void Seed(DataContext context)
    {
      if (context.Brands.Count() == 0)
      {
        List<Brand> brands = new List<Brand> {
          new Brand { Id = Guid.NewGuid(), Name = "Dell"},
          new Brand { Id = Guid.NewGuid(), Name = "Lenovo" },
          new Brand { Id = Guid.NewGuid(), Name = "Acer" },
          new Brand { Id = Guid.NewGuid(), Name = "Apple" },
          new Brand { Id = Guid.NewGuid(), Name = "Toshiba" },
          new Brand { Id = Guid.NewGuid(), Name = "Canon" },
          new Brand { Id = Guid.NewGuid(), Name = "Logitec" },
          new Brand { Id = Guid.NewGuid(), Name = "HP" },
          new Brand { Id = Guid.NewGuid(), Name = "TP-Link" },
          new Brand { Id = Guid.NewGuid(), Name = "D-Link" }
        };
        context.Brands.AddRange(brands);
      }

      if (context.Categories.Count() == 0)
      {
        List<Category> categories = new List<Category> {
          new Category { Id = Guid.NewGuid(), Name = "Desktop Computers"},
          new Category { Id = Guid.NewGuid(), Name = "Servers" },
          new Category { Id = Guid.NewGuid(), Name = "Monitors" },
          new Category { Id = Guid.NewGuid(), Name = "Accessories" },
          new Category { Id = Guid.NewGuid(), Name = "Network Equipments" },
          new Category { Id = Guid.NewGuid(), Name = "Printers and Scanners" },
          new Category { Id = Guid.NewGuid(), Name = "Laptops" }
        };
        context.Categories.AddRange(categories);
      }
    }

    // GET: api/Devices
    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        var devices = _service.FindAll();
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
