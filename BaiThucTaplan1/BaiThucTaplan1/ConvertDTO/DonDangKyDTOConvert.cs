using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;

namespace BaiThucTaplan1.ConvertDTO
{
    public class DonDangKyDTOConvert
    {
        public DonDangkyDTO EntityDonDangKyDTO(Dondangkys dondangkys)
        {
            return new DonDangkyDTO
            {
                dondangkyid = dondangkys.dondangkyid,
                ngayguidon = dondangkys.ngayguidon,
                ngayxuly = dondangkys.ngayxuly,
                nguoixuly = dondangkys.nguoixuly,
                trangthaidon = dondangkys.trangthaidon,
                daotrangid = dondangkys.daotrangid,
                phantuid = dondangkys.phantuid
            };
        }
    }
}
