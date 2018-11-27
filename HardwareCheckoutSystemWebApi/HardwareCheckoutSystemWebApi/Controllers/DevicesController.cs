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
    public class DevicesController : Controller
    {

        private readonly DeviceService _service;

        public DevicesController(DataContext context)
        {
            _service = new DeviceService(context);
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                List<Device> result = await _service.FindAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> FindById([FromRoute]Guid guid)
        {
            try
            {
                Device device = await _service.FindDeviceById(guid);
                if (device == null) { return NotFound(); }
                return Ok(device);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpGet("serialNumber/{sn}")]
        public async Task<IActionResult> FindBySerialNumber([FromRoute]string sn)
        {
            try
            {
                Device device = await _service.FindDeviceBySerialNumber(sn);
                if (device == null) { return NotFound(); }
                return Ok(device);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Device device)
        {
            try
            {
                await _service.Insert(device);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Device device)
        {
            try
            {
                if (await _service.Update(device))
                {
                    return Ok(device);
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
                await _service.DeleteDeviceById(guid);
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
