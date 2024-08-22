using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Services;
using System;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaRepository _hangHoaRepository;

        public HangHoaController(IHangHoaRepository hangHoaRepository)
        {
            _hangHoaRepository = hangHoaRepository;
        }

        [HttpGet]
        //page = 1: set gia tri mac dinh cua page la 1
        public IActionResult GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            return Ok(_hangHoaRepository.GetAll(search, from, to, sortBy, page));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                return Ok(_hangHoaRepository.GetByid(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateNew(HangHoaModel model)
        {
            try
            {
                var data = _hangHoaRepository.Add(model);
                return StatusCode(StatusCodes.Status201Created, data);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
