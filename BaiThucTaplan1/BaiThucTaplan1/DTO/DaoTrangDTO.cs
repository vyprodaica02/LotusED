using BaiThucTaplan1.Entity;
using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.DTO
{
    public class DaoTrangDTO
    {
        public int daotrangid { get; set; }
        public bool? daketthuc { get; set; }
        public string? noidung { get; set; }
        public string? noitochuc { get; set; }
        public int? sothanhvienthamgia { get; set; }
        public DateTime? thoigiantochuc { get; set; }
        public int? nguoitrutri { get; set; }
    }
}
