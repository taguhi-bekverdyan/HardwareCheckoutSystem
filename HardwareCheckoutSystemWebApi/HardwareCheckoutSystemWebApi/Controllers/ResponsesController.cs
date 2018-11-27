﻿using System;
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
    public class ResponsesController : Controller
    {
        private readonly ResponseService _service;

        #region Ctor

        public ResponsesController(DataContext context)
        {
            _service = new ResponseService(context);
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
        public async Task<IActionResult> GetResponseById([FromRoute]Guid guid)
        {
            try
            {
                Response response = await _service.FindResponseById(guid);
                if (response == null) { return NotFound(); }
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Response response)
        {
            try
            {
                await _service.Insert(response);
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
        public async Task<IActionResult> Update([FromBody]Response response)
        {
            try
            {
                if (await _service.Update(response))
                {
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion


    }
}
