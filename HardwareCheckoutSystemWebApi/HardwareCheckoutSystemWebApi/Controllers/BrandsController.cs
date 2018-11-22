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
    public class BrandsController : Controller
    {

        private readonly BrandService _service;

        #region ctor
        public BrandsController(DataContext context)
        {
            _service = new BrandService(context);

            if (_service.FindAll().Result.Count() == 0)
            {
                AddBrands(context);
            }

        }
        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Brand> brands = await _service.FindAll();
                return Ok(brands);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpGet("{guid}", Name = "GetBrand")]
        public async Task<IActionResult> GetBrandById([FromRoute]Guid guid)
        {
            try
            {
                Brand brand = await _service.FindBrandById(guid);
                if(brand == null)
                {
                    return NotFound();
                }

                return Ok(brand);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpGet("byName/{name}", Name = "GetBrandByName")]
        public async Task<IActionResult> GetBrandById([FromRoute]string name)
        {
            try
            {
                Brand brand = await _service.FindBrandByName(name);
                if (brand == null)
                {
                    return NotFound();
                }
                return Ok(brand);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }


        #endregion

        #region POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Brand brand)
        {
            try
            {
                await _service.Insert(brand);
                return Ok(brand);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region PUT
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Brand brand)
        {
            try
            {
                if (await _service.Update(brand))
                {
                    return Ok(brand);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region DELETE
        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid guid)
        {
            try
            {
                Brand result = await _service.FindBrandById(guid);
                if (result == null) { return NotFound(); }
                await _service.Delete(result);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region HelperFunctions

        private void AddBrands(DataContext context)
        {
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Apple" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Samsung" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Google" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Asus" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Intel" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Amd" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Lenovo" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Canon" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Genius" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "LG" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Xiaomi" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Dell" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Msi" });
            context.SaveChanges();
        }

        #endregion

    }
}
