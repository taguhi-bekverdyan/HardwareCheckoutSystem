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
    public class CategoriesController : Controller
    {

        #region props

        private readonly CategoryService _service;

        #endregion

        #region Ctor

        public CategoriesController(DataContext context)
        {
            _service = new CategoryService(context);
            if (_service.FindAll().Result.Count == 0)
            {
                AddCategories(context);
            }
        }



        #endregion

        #region GET
        [HttpGet]
        public async Task<IActionResult> FindAll()
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

        [HttpGet("{guid}",Name ="GetCategory")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid guid)
        {
            try
            {
                Category category = await _service.FindCategoryById(guid);
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
                Category category = await _service.FindCategoryByName(name);
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

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Category category)
        {
            try
            {
                await _service.Insert(category);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Category category)
        {
            try
            {
                if (await _service.Update(category))
                {
                    return Ok(category);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region DELETE

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid guid)
        {
            try
            {
                await _service.DeleteCategoryById(guid);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region HELPERS

        private void AddCategories(DataContext context)
        {
            context.Categories.Add(new Category() { Name = "CPU"});
            context.Categories.Add(new Category() { Name = "GPU" });
            context.Categories.Add(new Category() { Name = "RAM" });
            context.Categories.Add(new Category() { Name = "Notebook" });
            context.Categories.Add(new Category() { Name = "Tasblet" });
            context.Categories.Add(new Category() { Name = "PC" });
            context.Categories.Add(new Category() { Name = "Printer" });
            context.Categories.Add(new Category() { Name = "Monitor" });
            context.Categories.Add(new Category() { Name = "SSD" });
            context.Categories.Add(new Category() { Name = "HDD" });
            context.Categories.Add(new Category() { Name = "Mouse" });
            context.Categories.Add(new Category() { Name = "Keyboard" });
            context.SaveChanges();
        }

        #endregion

    }
}
