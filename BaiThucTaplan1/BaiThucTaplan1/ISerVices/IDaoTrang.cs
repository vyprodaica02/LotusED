using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.pagination;

namespace BaiThucTaplan1.ISerVices
{
    public interface IDaoTrang
    {
        ErrorMessage ThemDaoTrang(DaoTrang daoTrang);
        ErrorMessage XoaDaoTrang(int idDaoTrang);
        ErrorMessage SuaDaoTrang(DaoTrangDTO daoTrangDTO);
        List<DaoTrangDTO> GetDaoTrang(string? noiToChuc, int pageSize, int pageNumber);
        List<DaoTrangDTO> GetAllDaoTrang(string? noiToChuc);
    }
}
