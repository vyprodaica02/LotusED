using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.pagination;
using BaiThucTaplan1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BaiThucTaplan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuaController : ControllerBase
    {
        private readonly Ichua _ichua;
        public ChuaController()
        {
            _ichua = new ChuaServices();
        }
        [HttpPost("themChua")]
        public IActionResult ThemChua(Chuas chuas)
        {
            var res = _ichua.ThemChua(chuas);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("thanh công");
            }
            else
            {
                return BadRequest("Thất Bại");
            }
        }

        [HttpPut("suaChua")]
        public IActionResult SuaChua(Chuas chuas,int id) {
            var res = _ichua.SuaChua(chuas,id);
            if(res == ErrorMessage.ThanhCong) 
            { 
                return Ok("thanh công"); 
            }
            else
            {
                return BadRequest("Thất Bại");
            }
        }

        [HttpDelete("xoaChua")]
        public IActionResult XoaChua(int id)
        {
            var res = _ichua.XoaChua(id);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("thanh công");
            }
            else
            {
                return BadRequest("Thất Bại");
            }
        }

        [HttpGet("layChua")]
        public IActionResult GetChua(int pageSize, int pageNumber, string? Ten)
        {
            var res = _ichua.GetChua(Ten,  pageSize,pageNumber);
            return Ok(res);
        }

        [HttpGet("layAllChua")]
        public IActionResult GeALltChua( string? Ten)
        {
            var res = _ichua.GetAllChua(Ten);
            return Ok(res);
        }
    }
}
