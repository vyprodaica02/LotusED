using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;

namespace BaiThucTaplan1.ISerVices
{
    public interface IDonDangKy
    {
        ErrorMessage ThemDonDangky(Dondangkys donDangky);
        ErrorMessage XoaDonDangKy(int idDonDangky);
        ErrorMessage SuaDonDangKy(Dondangkys donDangky);
        ErrorMessage AgreeDonDangKy(int dondangkyid);
        ErrorMessage rejectDonDangKy(int dondangkyid);
        List<DonDangkyDTO> GetDonDangKy(int pageSize, int pageNumber);
        List<DonDangkyDTO> GetAllDonDangKy();
    }
}
