using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace BaiThucTaplan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Ilogin _login;
        public  readonly IConfiguration _configuration;
        private readonly appDbcontext dbContext;

        public LoginController(IConfiguration configuration)
        {
            _login = new LoginServices();
            _configuration = configuration;
            dbContext = new appDbcontext();
        }

        [HttpPost("dangnhap")]
         public IActionResult Login([FromBody] LoginModal loginModal)
        {
            var phatTu = dbContext.Phantus.FirstOrDefault(x => x.Email == loginModal.Email);

            if (phatTu == null)
            {
                return BadRequest("Email khong ton tai!");
            }
            else
            {
                if (phatTu.password == loginModal.password)
                {
                    string token = GenerateTocken(phatTu);

                    // Tạo một cookie mới với tên "jwtToken" để lưu trữ token
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true, // Cho phép cookie chỉ được truy cập qua HTTP (không thông qua JavaScript)
                        Expires = DateTime.Now.AddDays(1), // Đặt thời gian hết hạn của cookie
                    };

                    // Thêm token vào cookie
                    Response.Cookies.Append("jwtToken", token, cookieOptions);

                    return Ok(token);
                }
                else
                {
                    return BadRequest("Mat khau khong dung!");
                }
            }
        }

        
        //creat tokken
        private string GenerateTocken(Phantu phatTu)
        {
            List<Claim> claims = new List<Claim>
            {
               // new Claim("Email nguoi dung", phatTu.Email),//lấy ra data người dùng
                new Claim("phatuid", phatTu.phantuid.ToString()),
                new Claim(ClaimTypes.Role ,phatTu.roll),//tạo ra roll admin

            };
            var sercuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(sercuritykey, SecurityAlgorithms.HmacSha256);

            var tocken = new JwtSecurityToken(_configuration["JwtSettings:Issuer"], _configuration["JwtSettings:Audience"], 
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(tocken);
        }
      
    }
}
