using BaiThucTaplan1.Entity;
using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.DTO
{
    public class PhanTuDTO
    {
        public int phantuid { get; set; }
        public string? anhchup { get; set; }
        public bool? dahoantuc { get; set; }
        public string? Email { get; set; }
        public int? gioitinh { get; set; }
        public string? ho { get; set; }
        public DateTime? ngaycapnhat { get; set; }
        public DateTime? ngayketthuc { get; set; }
        public DateTime? ngaysinh { get; set; }
        public DateTime? ngayxuatgia { get; set; }
        public string? phapdanh { get; set; }
        public string? sodienthoai { get; set; }
        public string? roll { get; set; }
        public string? ten { get; set; }
        public string? tendem { get; set; }
        public bool? isActive { get; set; }
        public string? password { get; set; }
    }
}
