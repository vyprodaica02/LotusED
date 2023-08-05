using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.pagination;
using Microsoft.Extensions.Options;

namespace BaiThucTaplan1.ISerVices
{
    public interface Ilogin
    {
        ErrorMessage Login(LoginModal loginModal);
       Task<ErrorMessage> CreateUser(PhanTuDTO phanTuDTO,IFormFile file);
         List<PhanTuDTO> GetPhanTu(string? Ten, string? phapdanh, string? Email, int? gioitinh, int pageSize, int pageNumber);
         List<PhanTuDTO> GetAllPhanTu(string? Ten, string? phapdanh, string? Email, int? gioitinh);
        ErrorMessage SuaMk(string email, string mkcu, string newmk,string rollnumber);
        Task<ErrorMessage>SuaPhantu(Phantu phantu, IFormFile file);
        ErrorMessage XoaPHanTu(int id);
        public void SendEmail(string Email);

    }
}
