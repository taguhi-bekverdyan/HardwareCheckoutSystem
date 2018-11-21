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
    public class ResponsesController : Controller
    {

        private readonly ResponseService _service;

        #region main
        public ResponsesController(DataContext context)
        {
            _service = new ResponseService(context);

        }
        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAllResponses()
        {
            try
            {
                List<Response> requests = await _service.FindAll();
                return Ok(requests);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{guid}", Name = "GetResponse")]
        public async Task<IActionResult> GetResponseById([FromRoute]Guid guid)
        {
            try
            {
                Response response = await _service.FindById(guid);
                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        //[HttpGet("byName/{name}", Name = "GetRequestByName")]
        //public async Task<IActionResult> GetCategoryById([FromRoute]string name)
        //{
        //    try
        //    {
        //        Category category = await _service.FindByName(name);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(category);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e);
        //    }
        //}


        #endregion

        #region POST
        [HttpPost]
        public async Task<IActionResult> CreateResponse([FromBody]Response response)
        {
            try
            {
                await _service.Insert(response);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT
        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateResponse([FromRoute]Guid guid, [FromBody]Response response)
        {
            try
            {
                Response result = await _service.FindById(guid);
                if (result == null)
                {
                    return NotFound();
                }
                result.Id = response.Id;
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
        public async Task<IActionResult> DeleteResponse([FromRoute]Guid guid)
        {
            try
            {
                Response result = await _service.FindById(guid);
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
    }
}
