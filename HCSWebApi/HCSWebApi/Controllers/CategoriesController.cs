using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCSWebApi.Models;
using HCSWebApi.Models.Context;
using HCSWebApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardwareCheckoutSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {

        private readonly CategoryService _service;

        #region main
        public CategoriesController(DataContext context)
        {
            _service = new CategoryService(context);

            if (_service.FindAll().Result.Count() == 0)
            {
                AddCategories(context);
            }

        }
        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                List<Category> categories = await _service.FindAll();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{guid}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid guid)
        {
            try
            {
                Category category = await _service.FindById(guid);
                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("byName/{name}", Name = "GetCategoryByName")]
        public async Task<IActionResult> GetCategoryByName([FromRoute]string name)
        {
            try
            {
                Category category = await _service.FindByName(name);
                if(category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


        #endregion

        #region POST
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]Category category)
        {
            try
            {
                await _service.Insert(category);
                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT
        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]Guid guid, [FromBody]Category category)
        {
            try
            {
                Category result = await _service.FindById(guid);
                if (result == null)
                {
                    return NotFound();
                }
                result.Name = category.Name;
                await _service.Update(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region DELETE
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]Guid guid)
        {
            try
            {
                Category result = await _service.FindById(guid);
                if (result == null)
                {
                    return NotFound();
                }
                await _service.Delete(result);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region Helpers

        private void AddCategories(DataContext context)
        {
            context.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = "Laptops" });
            context.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = "Monitors" });
            context.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = "Keyboards" });
            context.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = "Servers" });
            context.Categories.Add(new Category() { Id = Guid.NewGuid(), Name = "Printers" });
            context.SaveChanges();
        }
        #endregion
    }
}
