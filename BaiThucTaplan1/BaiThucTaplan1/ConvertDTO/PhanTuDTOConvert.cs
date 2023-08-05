using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;

namespace BaiThucTaplan1.ConvertDTO
{
    public class PhanTuDTOConvert
    {
        public PhanTuDTO EntityDTO(Phantu phantu)
        {
            return new PhanTuDTO
            {
                phantuid = phantu.phantuid,
                anhchup = phantu.anhchup,
                dahoantuc = phantu.dahoantuc,
                Email = phantu.Email,
                gioitinh = phantu.gioitinh,
                ho = phantu.ho,
                ngaycapnhat = phantu.ngaycapnhat,
                ngayketthuc = phantu.ngayketthuc,
                ngaysinh = phantu.ngaysinh,
                ngayxuatgia = phantu.ngayxuatgia,
                phapdanh = phantu.phapdanh,
                sodienthoai = phantu.sodienthoai,
                roll = phantu.roll,
                ten = phantu.ten,
                tendem = phantu.tendem,
                isActive = phantu.isActive
            };
        }
    }
}
