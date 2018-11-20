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
  public class BrandsController :ControllerBase
  {
    private BrandService _service { get; set; }

    public BrandsController(DataContext context)
    {
      _service = new BrandService(context);

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
    }

    // GET: api/Brands
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var brands = await _service.FindAll();
        if (brands != null)
        {
          return Ok(brands);
        }
        else return NotFound(brands);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // GET: api/Brands/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      try
      {
        var brands = await _service.FindById(id);
        if (brands != null)
        {
          return Ok(brands);
        }
        else return NotFound(brands);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // POST: api/Brands
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Brand brand)
    {
      try
      {
        await _service.Insert(brand);
        return Created(brand.Id.ToString(), brand);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // PUT: api/Brands/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Brand brand)
    {
      try
      {
        await _service.Update(brand);
        return Ok(brand);
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
