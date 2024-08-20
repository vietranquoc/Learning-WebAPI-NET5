using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models;
using System.Linq;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaiController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dsLoai = _context.Loais.ToList(); 
            return Ok(dsLoai);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (loai == null)
            {
                return NotFound();
            } else
            {
                return Ok(loai);
            }
        }

        [HttpPost]
        public IActionResult CreateNew(LoaiVM model)
        {
            try
            {
                var loai = new Loai
                {
                    TenLoai = model.TenLoai,
                };

                _context.Add(loai);
                _context.SaveChanges();

                return Ok(loai);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, LoaiVM model)
        {
            try
            {
                var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == id);
                if (loai == null)
                {
                    return NotFound();
                } 
                else
                {
                    loai.TenLoai = model.TenLoai;
                    _context.SaveChanges();
                    return Ok(loai);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == id);
                if (loai == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Remove(loai);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        Des = "Remove Success"
                    });
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
