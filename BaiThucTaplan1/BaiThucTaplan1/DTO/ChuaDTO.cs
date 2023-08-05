using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.DTO
{
    public class ChuaDTO
    {
        public int chuaid { get; set; }
        public DateTime? capnhat { get; set; }
        public string? diachi { get; set; }
        public DateTime? ngaythanhlap { get; set; }
        public string? tenchua { get; set; }
        public int? trutri { get; set; }
    }
}
