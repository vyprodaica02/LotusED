using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;

namespace BaiThucTaplan1.ConvertDTO
{
    public class DaoTrangDTOConvert
    {
        public DaoTrangDTO EntityDaoTrangDTO(DaoTrang daoTrang)
        {
            return new DaoTrangDTO
            {
                daotrangid = daoTrang.daotrangid,
                daketthuc = daoTrang.daketthuc,
                noidung = daoTrang.noidung,
                noitochuc = daoTrang.noitochuc,
                sothanhvienthamgia = daoTrang.sothanhvienthamgia,
                thoigiantochuc = daoTrang.thoigiantochuc,
                nguoitrutri = daoTrang.nguoitrutri,
            };
        }
    }
}
