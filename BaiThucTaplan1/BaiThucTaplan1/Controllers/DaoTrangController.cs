using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.pagination;
using BaiThucTaplan1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaiThucTaplan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaoTrangController : ControllerBase
    {
        private readonly IDaoTrang _daoTrang;

        public DaoTrangController()
        {
            _daoTrang = new DaoTrangServices();
        }

        [HttpPost("ThemDaoTrang")]
        public IActionResult ThemDaoTrang(DaoTrang daoTrang)
        {
            var res = _daoTrang.ThemDaoTrang(daoTrang);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Thêm thành công");
            }
            else
            {
                return BadRequest("thêm thất bại");
            }
        }
        [HttpDelete("xoaDaoTrang")]
        public IActionResult XoaDaoTrang(int idDaoTrang)
        {
            var res = _daoTrang.XoaDaoTrang(idDaoTrang);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Xóa thành công");
            }
            else
            {
                return BadRequest("Xóa thất bại");
            }
        }
        [HttpPost("SuaDaotrang")]
        public IActionResult SuaDaoTrang(DaoTrangDTO daoTrangDTO)
        {
            var res = _daoTrang.SuaDaoTrang(daoTrangDTO);
            if (res == ErrorMessage.ThanhCong)
            {
                return Ok("Sửa thành công");
            }
            else
            {
                return BadRequest("Sửa thất bại");
            }
        }
        [HttpGet("layDanhSachDaoTrang")]
        public IActionResult GetDaoTrang(string? noiToChuc,  int pageSize, int pageNumber)
        {
            var res = _daoTrang.GetDaoTrang(noiToChuc, pageSize,pageNumber);
            return Ok(res);
        }

        [HttpGet("layAllDanhSachDaoTrang")]
        public IActionResult GetDaoTrang(string? noiToChuc)
        {
            var res = _daoTrang.GetAllDaoTrang(noiToChuc);
            return Ok(res);
        }

    }
}
