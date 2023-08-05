using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.Entity
{
    public class Phantudaotrangs
    {
        [Key] public int phantudaotrangid { get; set; }
        public bool? dathamgia { get; set; }
        public string? lydokhongthamgia { get; set; }
        public int? daotrangid { get; set; }
        public DaoTrang? daotrang { get; set; }
        public int? phantuid { get; set; }
        public Phantu? phantu { get; set; }
    }
}
