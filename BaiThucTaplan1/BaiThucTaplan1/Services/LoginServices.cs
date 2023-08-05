using BaiThucTaplan1.ConvertDTO;
using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.pagination;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Text;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BaiThucTaplan1.Services
{
    public class LoginServices : Ilogin
    {
        private readonly appDbcontext dbContext;
        private readonly PhanTuDTOConvert phanTuDTOConvert;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public LoginServices()
        {
            this.dbContext = new appDbcontext();
            phanTuDTOConvert = new PhanTuDTOConvert();
            _httpContextAccessor = new HttpContextAccessor();
            // Tạo và cấu hình IConfiguration bên trong hàm tạo
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Thay đổi thành đường dẫn tùy ý
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Thay đổi tên tệp cấu hình nếu cần
            .Build();
        }

        public async Task<ErrorMessage> CreateUser(PhanTuDTO phanTuDTO, IFormFile file)
        {
            var PhantuHt = dbContext.Phantus.FirstOrDefault(x => x.Email == phanTuDTO.Email);
            if (PhantuHt != null)
            {
                return ErrorMessage.ThatBai;
            }
            else
            {
                PhantuHt = new Phantu();
                // Kiểm tra và xử lý tệp tin ảnh (nếu có)
                if (file != null && file.Length > 0)
                {
                    string imageUrl = await uploadfile.UploadFile(file);
                    phanTuDTO.anhchup = imageUrl;
                    PhantuHt.anhchup = phanTuDTO.anhchup;
                }
                // Tiếp tục xử lý thông tin người dùng và lưu entity Phantu vào cơ sở dữ liệu thông qua Entity Framework
                phanTuDTO.isActive = true;
                PhantuHt.dahoantuc = phanTuDTO.dahoantuc;
                PhantuHt.Email = phanTuDTO.Email;
                PhantuHt.gioitinh = phanTuDTO.gioitinh;
                PhantuHt.ho = phanTuDTO.ho;
                PhantuHt.ngaycapnhat = phanTuDTO.ngaycapnhat;
                PhantuHt.ngayketthuc = phanTuDTO.ngayketthuc;
                PhantuHt.ngaysinh = phanTuDTO.ngaysinh;
                PhantuHt.ngayxuatgia = phanTuDTO.ngayxuatgia;
                PhantuHt.phapdanh = phanTuDTO.phapdanh;
                PhantuHt.sodienthoai = phanTuDTO.sodienthoai;
                PhantuHt.ten = phanTuDTO.ten;
                PhantuHt.tendem = phanTuDTO.tendem;
                PhantuHt.isActive = phanTuDTO.isActive;

                dbContext.Add(PhantuHt);
                dbContext.SaveChanges();
                return ErrorMessage.ThanhCong;
            }
        }


        //lấy phần tử
        public List<PhanTuDTO> GetPhanTu(string? Ten, string? phapdanh, string? Email, int? gioitinh, int pageSize, int pageNumber)
        {

            var check = dbContext.Phantus.OrderByDescending(x => x.ten).AsQueryable();
            if (!string.IsNullOrEmpty(Ten))
            {
                check = check.Where(x => x.ten.ToLower().Contains(Ten.ToLower()) && x.isActive == true).AsQueryable();
            }
            if (!string.IsNullOrEmpty(phapdanh))
            {
                check = check.Where(x => x.phapdanh.ToLower().Contains(phapdanh.ToLower()) && x.isActive == true).AsQueryable();
            }
            if (!string.IsNullOrEmpty(Email))
            {
                check = check.Where(x => x.Email.ToLower().Contains(Email.ToLower()) && x.isActive == true).AsQueryable();
            }
            if (gioitinh.HasValue)
            {
                string gioiTinhValue = gioitinh.Value.ToString();
                check = check.Where(x => x.gioitinh.ToString().Contains(gioiTinhValue) && x.isActive == true).AsQueryable();
            }

            var pagingHelper = new PagingHelper<PhanTuDTO>(pageSize, pageNumber);
            var pagedResult = pagingHelper.GetPagedResult(check.Select(x => phanTuDTOConvert.EntityDTO(x)).ToList());

            return pagedResult;
        }

        public List<PhanTuDTO> GetAllPhanTu(string? Ten, string? phapdanh, string? Email, int? gioitinh)
        {
            var check = dbContext.Phantus.OrderByDescending(x => x.ten).AsQueryable();
            if (!string.IsNullOrEmpty(Ten))
            {
                check = check.Where(x => x.ten.ToLower().Contains(Ten.ToLower()) && x.isActive == true).AsQueryable();
            }
            if (!string.IsNullOrEmpty(phapdanh))
            {
                check = check.Where(x => x.phapdanh.ToLower().Contains(phapdanh.ToLower()) && x.isActive == true).AsQueryable();
            }
            if (!string.IsNullOrEmpty(Email))
            {
                check = check.Where(x => x.Email.ToLower().Contains(Email.ToLower()) && x.isActive == true).AsQueryable();
            }
            if (gioitinh.HasValue)
            {
                string gioiTinhValue = gioitinh.Value.ToString();
                check = check.Where(x => x.gioitinh.ToString().Contains(gioiTinhValue) && x.isActive == true).AsQueryable();
            }
            var query = check.OrderByDescending(x => x.ten).Select(x => phanTuDTOConvert.EntityDTO(x)).ToList();
            return query;
        }
        //đăng nhập
        public ErrorMessage Login(LoginModal loginModal)
        {
            var usr = dbContext.Phantus
                   .FirstOrDefault(x => x.Email == loginModal.Email && x.password == loginModal.password);
            if (usr == null)//khong dung nguoi dung
            {
                return ErrorMessage.ThatBai;
            }
            return ErrorMessage.ThanhCong;
        }
        //sửa mk
        public ErrorMessage SuaMk(string Email, string mkcu, string newmk, string rollnumber)
        {
            var emaiht = dbContext.Phantus.FirstOrDefault(x => x.Email == Email);
            var rollnumberHT = dbContext.rolllNumbers.FirstOrDefault(x => x.roll == rollnumber);

            if (emaiht != null && rollnumberHT != null)
            {
                if (dbContext.Phantus.Any(x => x.Email == Email && x.password == mkcu))
                {
                    emaiht.password = newmk;
                    dbContext.Update(emaiht);
                    dbContext.SaveChanges();
                    return ErrorMessage.ThanhCong;
                }
                else
                {
                    return ErrorMessage.ThatBai;
                }
            }
            return ErrorMessage.ThatBai;
        }
        //sửa phần tử
        [Authorize(Roles = "admin")] //tk là admin thì dc thực hiên
        public async Task<ErrorMessage> SuaPhantu(Phantu phantu, IFormFile file)
        {
            var phanTuHT = dbContext.Phantus.FirstOrDefault(x => x.phantuid == phantu.phantuid);
            if (phanTuHT != null)
            {
                phanTuHT.dahoantuc = phantu.dahoantuc;
                phanTuHT.Email = phantu.Email;
                phanTuHT.gioitinh = phantu.gioitinh;
                phanTuHT.ho = phantu.ho;
                phanTuHT.ngaycapnhat = phantu.ngaycapnhat;
                phanTuHT.ngayketthuc = phantu.ngayketthuc;
                phanTuHT.ngaysinh = phantu.ngaysinh;
                phanTuHT.ngayxuatgia = phantu.ngayxuatgia;
                phanTuHT.phapdanh = phantu.phapdanh;
                phanTuHT.sodienthoai = phantu.sodienthoai;
                phanTuHT.ten = phantu.ten;
                phanTuHT.tendem = phantu.tendem;

                // Kiểm tra và xử lý tệp tin ảnh (nếu có)
                if (file != null && file.Length > 0)
                {
                    string imageUrl = await uploadfile.UploadFile(file);
                    phanTuHT.anhchup = imageUrl;
                }

                dbContext.Update(phanTuHT);
                dbContext.SaveChanges();
                return ErrorMessage.ThanhCong;
            }
            return ErrorMessage.ThatBai;
        }
        //xoa phan tử
        [Authorize(Roles = "admin")] //tk là admin thì dc thực hiên
        public ErrorMessage XoaPHanTu(int id)
        {
            var Emailht = dbContext.Phantus.FirstOrDefault(x => x.phantuid == id);

            if (Emailht != null)
            {
                Emailht.isActive = false;
                dbContext.Update(Emailht);
                dbContext.SaveChanges();
                return ErrorMessage.ThanhCong;
            }
            return ErrorMessage.ThatBai;
        }

        public void SendEmail(string Email)
        {
            var EmailHT = dbContext.Phantus.FirstOrDefault(x => x.Email == Email);
            var rollnumber = dbContext.rolllNumbers.FirstOrDefault(x => x.phantuid == EmailHT.phantuid);
            if (EmailHT != null)
            {
                Random random = new Random();
                string randomNumber = random.Next(1000, 1000001).ToString();

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("duc.luongming@gmail.com"));
                email.To.Add(MailboxAddress.Parse(Email));
                email.Subject = "Ma tocken Email cua ban";
                email.Body = new TextPart(TextFormat.Html) { Text = randomNumber };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("duc.luongming@gmail.com", "xdsbimadompaojqi");
                smtp.Send(email);
                smtp.Disconnect(true);
                rollnumber.roll = randomNumber;
                dbContext.Update(rollnumber);
                dbContext.SaveChanges();
            }
        }


    }
}
