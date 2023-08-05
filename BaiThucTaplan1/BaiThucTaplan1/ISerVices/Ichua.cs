using BaiThucTaplan1.ConvertDTO;
using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.pagination;

namespace BaiThucTaplan1.ISerVices
{
    public interface Ichua
    {
        
        ErrorMessage ThemChua(Chuas chuas);
        ErrorMessage SuaChua(Chuas chuas,int id);
        ErrorMessage XoaChua( int id);
        List<ChuaDTO> GetChua(string? Ten, int pageSize, int pageNumber);
        List<ChuaDTO> GetAllChua(string? Ten);

    }
}
