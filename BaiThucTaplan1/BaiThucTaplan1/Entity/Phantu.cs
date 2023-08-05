using System.ComponentModel.DataAnnotations;
using System;

namespace BaiThucTaplan1.Entity
{
    public class Phantu
    {
        [Key] public int phantuid { get; set; }
        public string? anhchup { get; set; }
        public bool? dahoantuc { get; set; }
        public string? Email { get; set; }
        public int? gioitinh { get; set; }
        public string? ho { get; set; }
        public DateTime? ngaycapnhat { get; set; }
        public DateTime? ngayketthuc { get; set; }
        public DateTime? ngaysinh { get; set; }
        public DateTime? ngayxuatgia { get; set; }
        public string? password { get; set; }
        public string? phapdanh { get; set; }
        public string? sodienthoai { get; set; }
        public string? ten { get; set; }
        public string? tendem { get; set; }
        public string? roll { get; set; }
        public bool? isActive { get; set; }
        public int? chuaid { get; set; }
        public Chuas? chuas { get; set; }
        public int? kieuthanhvienid { get; set; }
        public Kieuthanhviens? kieuthanhviens { get; set; }
        public IEnumerable<Phantudaotrangs>? phantudaotrangs { get; set; }
        public IEnumerable<Dondangkys>? dondangkys { get; set; }
    }
}
