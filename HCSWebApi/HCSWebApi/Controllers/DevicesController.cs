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
    public class DevicesController : Controller
    {

        private readonly DeviceService _service;

        #region main
        public DevicesController(DataContext context)
        {
            _service = new DeviceService(context);

        }
        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Device> devices = await _service.FindAll();
                return Ok(devices);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{guid}", Name = "GetDevice")]
        public async Task<IActionResult> GetDeviceById([FromRoute]Guid guid)
        {
            try
            {
                Device device = await _service.FindById(guid);
                if (device == null)
                {
                    return NotFound();
                }

                return Ok(device);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("byName/{name}", Name = "GetDeviceByName")]
        public async Task<IActionResult> GetDeviceBySN([FromRoute]string sn)
        {
            try
            {
                Device device = await _service.FindBySn(sn);
                if (device == null)
                {
                    return NotFound();
                }
                return Ok(device);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


        #endregion

        #region POST
        [HttpPost]
        public async Task<IActionResult> CreateDevice([FromBody]Device device)
        {
            try
            {
                await _service.Insert(device);
                return Ok(device);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT
        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateDevice([FromRoute]Guid guid, [FromBody]Device device)
        {
            try
            {
                Device result = await _service.FindById(guid);
                if (result == null)
                {
                    return NotFound();
                }
                result.SerialNumber = device.SerialNumber;
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
        public async Task<IActionResult> DeleteDevice([FromRoute]Guid guid)
        {
            try
            {
                Device result = await _service.FindById(guid);
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