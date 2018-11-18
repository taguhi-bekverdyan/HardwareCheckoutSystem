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
  public class CategoriesController : ControllerBase
  {
    private CategoryService _service { get; set; }

    public CategoriesController(DataContext context)
    {
      _service = new CategoryService(context);
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var categories = await _service.FindAll();
        if (categories != null)
        {
          return Ok(categories);
        }
        else return NotFound(categories);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      try
      {
        var categories = await _service.FindById(id);
        if (categories != null)
        {
          return Ok(categories);
        }
        else return NotFound(categories);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Category category)
    {
      try
      {
        await _service.Insert(category);
        return Created(category.Id.ToString(), category);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    // PUT: api/Categories/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Category category)
    {
      try
      {
        await _service.Update(category);
        return Ok(category);
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
