using BaiThucTaplan1.Entity;
using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.DTO
{
    public class DonDangkyDTO
    {
        public int dondangkyid { get; set; }
        public DateTime? ngayguidon { get; set; }
        public DateTime? ngayxuly { get; set; }
        public int? nguoixuly { get; set; }
        public int? trangthaidon { get; set; }
        public int? daotrangid { get; set; }
        public int? phantuid { get; set; }
    }
}
