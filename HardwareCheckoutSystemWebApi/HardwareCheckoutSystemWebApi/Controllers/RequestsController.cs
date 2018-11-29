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
    public class RequestsController : Controller
    {

        private readonly RequestService _service;

        #region Ctor

        public RequestsController(DataContext context)
        {
            _service = new RequestService(context);
        }

        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.FindAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetRequestById([FromRoute]Guid guid)
        {
            try
            {
                Request request = await _service.FindRequestById(guid);
                if (request == null) { return NotFound(); }
                return Ok(request);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("inPending")]
        public async Task<IActionResult> GetRequestsInPending()
        {
            try
            {
                List<Request> requests = await _service.FindRequestsInPending();
                return Ok(requests);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Request request)
        {
            try
            {
                await _service.Insert(request);
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
        public async Task<IActionResult> Update([FromBody]Request request)
        {
            try
            {
                if (await _service.Update(request))
                {
                    return Ok(request);
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
                await _service.DeleteRequestById(guid);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

    }
}
