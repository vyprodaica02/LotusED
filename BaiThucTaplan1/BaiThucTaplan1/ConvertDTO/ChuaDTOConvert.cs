using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;

namespace BaiThucTaplan1.ConvertDTO
{
    public class ChuaDTOConvert
    {
        public ChuaDTO EntityChuaDTO(Chuas chuas)
        {
            return new ChuaDTO
            {
                chuaid = chuas.chuaid,
                capnhat = chuas.capnhat,
                diachi = chuas.diachi,
                ngaythanhlap = chuas.ngaythanhlap,
                tenchua = chuas.tenchua,
                trutri = chuas.trutri,
            };
        }
    }
}
