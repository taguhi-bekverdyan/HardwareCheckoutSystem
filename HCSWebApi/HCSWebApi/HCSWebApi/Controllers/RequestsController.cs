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
    public class RequestsController : Controller
    {

        private readonly RequestService _service;

        #region main
        public RequestsController(DataContext context)
        {
            _service = new RequestService(context);

        }
        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                List<Request> requests = await _service.FindAll();
                return Ok(requests);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{guid}", Name = "GetRequest")]
        public async Task<IActionResult> GetRequestById([FromRoute]Guid guid)
        {
            try
            {
                Request request = await _service.FindById(guid);
                if (request == null)
                {
                    return NotFound();
                }

                return Ok(request);
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
        public async Task<IActionResult> CreateRequest([FromBody]Request request)
        {
            try
            {
                await _service.Insert(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT
        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateRequest([FromRoute]Guid guid, [FromBody]Request request)
        {
            try
            {
                Request result = await _service.FindById(guid);
                if (result == null)
                {
                    return NotFound();
                }
                result.Id = request.Id;
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
        public async Task<IActionResult> DeleteRequest([FromRoute]Guid guid)
        {
            try
            {
                Request result = await _service.FindById(guid);
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
