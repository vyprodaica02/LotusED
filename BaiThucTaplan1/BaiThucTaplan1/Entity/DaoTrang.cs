using BaiThucTaplan1.DTO;
using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.Entity
{
    public class DaoTrang
    {
        [Key] public int daotrangid { get; set; }
        public bool? daketthuc { get; set; }
        public string? noidung { get; set; }
        public string? noitochuc { get; set; }
        public int? sothanhvienthamgia { get; set; }
        public DateTime? thoigiantochuc { get; set; }
        public int? nguoitrutri { get; set; }
        public IEnumerable<Phantudaotrangs>? phantudaotrangs { get; set; }
        public IEnumerable<Dondangkys>? dondangkys { get; set; }

        
    }
}
