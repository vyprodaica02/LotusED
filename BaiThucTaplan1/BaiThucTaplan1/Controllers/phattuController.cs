using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.pagination;
using BaiThucTaplan1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BaiThucTaplan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class phattuController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly Ilogin _ilogin;
        public phattuController(IConfiguration configuration)
        {
            _configuration = configuration;
            _ilogin = new LoginServices();
        }

        [HttpGet("layphantu")]
        [Authorize]
        public IActionResult Get(int pageSize, int pageNumber, string? Ten, string? phapdanh, string? email, int? gioitinh)
        {
            var res = _ilogin.GetPhanTu(Ten, phapdanh, email, gioitinh, pageSize, pageNumber);
            return Ok(res);
        }

        [HttpGet("layALLphantu")]
        [Authorize]
        public IActionResult GetAll(string? Ten, string? phapdanh, string? email, int? gioitinh)
        {
            var res = _ilogin.GetAllPhanTu(Ten, phapdanh, email, gioitinh);
            return Ok(res);
        }
        [HttpPut("suaMk")]
        [Authorize]
        public IActionResult SuaMK(string Email, string mkcu, string newmk, string rollnumber)
        {
            var res = _ilogin.SuaMk(Email, mkcu, newmk, rollnumber);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("thành công");
            }
            else
            {
                return BadRequest("Thất Bại");
            }
        }
        [HttpPut("SuaPhantu")]
        [Authorize]
        public async Task<IActionResult> SuaPhanTu([FromForm] Phantu phantu, IFormFile file)
        {
            var res = await _ilogin.SuaPhantu(phantu, file);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Thành công");
            }
            else
            {
                return BadRequest("Thất bại");
            }
        }
        [HttpPost("dangky")]
        public async Task<IActionResult> DAngky([FromForm] PhanTuDTO phanTuDTO,  IFormFile file)
        {
            var res = await _ilogin.CreateUser(phanTuDTO, file);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Thành công");
            }
            else
            {
                return BadRequest("Thất bại");
            }
        }
        [HttpPut("XoaPhantu")]
        [Authorize]
        public IActionResult XoahanTu(int id)
        {
            var ret = _ilogin.XoaPHanTu(id);
            if (ret == ErrorMessage.ThanhCong)
            {
                return Ok("thanh công");
            }
            else
            {
                return BadRequest(ret);


            }
        }
        [HttpPost("sendEmail")]
        public IActionResult sendEmail(string Email)
        {
            _ilogin.SendEmail(Email);
            return Ok("thanh công");
        }


    }
}
