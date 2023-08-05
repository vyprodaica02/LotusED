using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaiThucTaplan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonDangKyController : ControllerBase
    {
        private readonly IDonDangKy _dondangky;

        public DonDangKyController()
        {
            _dondangky = new DonDangKyServices();
        }
        [Authorize]
        [HttpPost("ThemDonDangKy")]
        public IActionResult ThemDonDangKy(Dondangkys dondangkys)
        {
            var res = _dondangky.ThemDonDangky(dondangkys);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Thêm thành công");
            }
            else
            {
                return BadRequest("thêm thất bại");
            }
        }
        [HttpDelete("xoaDonDangKy")]
        public IActionResult XoaDonDangKy(int iddondangky)
        {
            var res = _dondangky.XoaDonDangKy(iddondangky);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Xóa thành công");
            }
            else
            {
                return BadRequest("Xóa thất bại");
            }
        }

        [HttpPut("SuaDonDangKy")]
        public IActionResult SuaDonDangKy(Dondangkys dondangkys)
        {
            var res = _dondangky.SuaDonDangKy(dondangkys);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Sửa thành công");
            }
            else
            {
                return BadRequest("Sửa thất bại");
            }
        }
        [Authorize]
        [HttpPut("Đồng ý Đơn")]
        public IActionResult AgreeDonDangKy(int dondangkyid)
        {
            var res = _dondangky.AgreeDonDangKy(dondangkyid);
            if(res == ErrorMessage.ThanhCong)
            {
                return Ok("Đơn đã được đồng ý");
            }else if(res == ErrorMessage.TrangThaiDonDaDuocXoa)
            {
                return BadRequest("Đơn Đã được xóa");
            }
            else if(res == ErrorMessage.DonDangKyKhongTonTai)
            {
                return BadRequest("Đơn Không tồn tại");
            }
            else
            {
                return BadRequest("Đơn Không tồn tại");

            }
        }

        [Authorize]
        [HttpPut("từ chối Đơn")]
        public IActionResult RejectDonDangKy(int dondangkyid)
        {
            var res = _dondangky.rejectDonDangKy(dondangkyid);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Đơn đã được từ chối");
            }
            else if (res == ErrorMessage.TrangThaiDonDaDuocDuyet)
            {
                return BadRequest("Đơn Đã được duyệt");
            }
            else if (res == ErrorMessage.DonDangKyKhongTonTai)
            {
                return BadRequest("Đơn Không tồn tại");
            }
            else
            {
                return BadRequest("Đơn Không tồn tại");

            }
        }
        [Authorize]
        [HttpGet("layAllDanhSachDonDangKy")]
        public IActionResult GetAllDonDangKy()
        {
            var res = _dondangky.GetAllDonDangKy();
            return Ok(res);
        }
        [Authorize]
        [HttpGet("layDanhSachDonDangKy")]
        public IActionResult GetDonDangKy( int pageSize, int pageNumber)
        {
            var res = _dondangky.GetDonDangKy( pageSize, pageNumber);
            return Ok(res);
        }
    }
}
