using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.Entity
{
    public class Dondangkys
    {
        [Key] public int dondangkyid { get; set; }
        public DateTime? ngayguidon { get; set; }
        public DateTime? ngayxuly { get; set; }
        public int? nguoixuly { get; set; }
        public int? trangthaidon { get; set; }
        public int? daotrangid { get; set; }
        public DaoTrang? daotrangs { get; set; }
        public int? phantuid { get; set; }
        public Phantu? phantu { get; set; }
    }
}
