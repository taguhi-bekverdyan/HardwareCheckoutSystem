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
  public class BrandsController
  {
    private BrandService _service { get; set; }

    public BrandsController(DataContext context)
    {
      _service = new BrandService(context);
    }

    // GET: api/Brands
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET: api/Brands/5
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id)
    {
      return "value";
    }

    // POST: api/Brands
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT: api/Brands/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
