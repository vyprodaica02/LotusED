using BaiThucTaplan1.ConvertDTO;
using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaiThucTaplan1.Services
{
    public class DonDangKyServices : IDonDangKy
    {
        private readonly appDbcontext dbContext;
        private readonly DonDangKyDTOConvert donDangKyDTOConvert;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DonDangKyServices()
        {
            this.dbContext = new appDbcontext();
            donDangKyDTOConvert = new DonDangKyDTOConvert();
            _httpContextAccessor = new HttpContextAccessor();
        }

        [Authorize(Roles = "admin")] //tk là admin thì dc thực hiên
        public List<DonDangkyDTO> GetAllDonDangKy()
        {
            var check = dbContext.Dondangkys.AsQueryable();
            var query = check.Select(x => donDangKyDTOConvert.EntityDonDangKyDTO(x)).ToList();
            return query;
        }

        [Authorize(Roles = "admin")] //tk là admin thì dc thực hiên
        public List<DonDangkyDTO> GetDonDangKy(int pageSize, int pageNumber)
        {
            var check = dbContext.Dondangkys.AsQueryable();

            var pagingHelper = new PagingHelper<DonDangkyDTO>(pageSize, pageNumber);
            var pagedResult = pagingHelper.GetPagedResult(check.Select(x => donDangKyDTOConvert.EntityDonDangKyDTO(x)).ToList());
            return pagedResult;
        }

        public ErrorMessage SuaDonDangKy(Dondangkys donDangky)
        {
            var dondangkyHT = dbContext.Dondangkys.FirstOrDefault(x => x.dondangkyid == donDangky.dondangkyid);
            if (dondangkyHT != null)
            {
                dondangkyHT.dondangkyid = donDangky.dondangkyid;
                dondangkyHT.ngayguidon = donDangky.ngayguidon;
                dondangkyHT.ngayxuly = donDangky.ngayxuly;
                dondangkyHT.nguoixuly = donDangky.nguoixuly;
                dondangkyHT.trangthaidon = donDangky.trangthaidon;
                dondangkyHT.daotrangid = donDangky.daotrangid;
                dondangkyHT.phantuid = donDangky.phantuid;
                dbContext.Update(dondangkyHT);
                dbContext.SaveChanges();
                return ErrorMessage.ThanhCong;
            }
            return ErrorMessage.ThatBai;
        }
        private void CapNhatThanhVien(int daotrangid)
        {
            var daoTrangHT = dbContext.daoTrangs.FirstOrDefault(x => x.daotrangid == daotrangid);
            if (daoTrangHT != null)
            {
                daoTrangHT.sothanhvienthamgia = dbContext.Dondangkys.Count(x => x.daotrangid == daoTrangHT.daotrangid);
                dbContext.Update(daoTrangHT);
                dbContext.SaveChanges();
            }
        }
        public ErrorMessage ThemDonDangky(Dondangkys donDangky)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                var lstDondangky = donDangky.daotrangs;
                var lstPhanTu = donDangky.phantu;
                donDangky.phantu = null;
                donDangky.daotrangs = null;

                if (!dbContext.daoTrangs.Any(x => x.daotrangid == donDangky.daotrangid))
                {
                    return ErrorMessage.DaoTrangKhongTonTai;
                }

                var dsDonDangKy = dbContext.Dondangkys.AsQueryable();
                foreach (var item in dsDonDangKy)
                {
                    if (item.daotrangid == donDangky.daotrangid && item.phantuid == donDangky.phantuid)
                    {
                        return ErrorMessage.PhatTuThamGiaDaoTrangDaTonTai;
                    }
                }
                var authenticatedUser = _httpContextAccessor.HttpContext.User;
                var phattuidClaim = authenticatedUser.FindFirst("phatuid");
                if (phattuidClaim != null && int.TryParse(phattuidClaim.Value, out int phattuid))
                {
                    donDangky.ngayxuly = DateTime.Now;
                    donDangky.trangthaidon = 1;
                    donDangky.phantuid = phattuid;
                    dbContext.Add(donDangky);
                    dbContext.SaveChanges();
                    trans.Commit();
                    return ErrorMessage.ThanhCong;
                }
                else
                {
                    return ErrorMessage.PhatTuChuaDangNhap;
                }
            }
        }

        public ErrorMessage XoaDonDangKy(int idDonDangky)
        {
            var dondangkyHT = dbContext.Dondangkys.FirstOrDefault(x => x.dondangkyid == idDonDangky);
            if (dondangkyHT != null)
            {
                dbContext.Remove(dondangkyHT);
                dbContext.SaveChanges();
                CapNhatThanhVien((int)dondangkyHT.daotrangid);
                return ErrorMessage.ThanhCong;
            }
            return ErrorMessage.ThatBai;
        }
        //đồng ý đơn
        [Authorize(Roles = "admin")]
        public ErrorMessage AgreeDonDangKy(int dondangkyid)
        {
            var dondangkyht = dbContext.Dondangkys.FirstOrDefault(x => x.dondangkyid == dondangkyid);
            if (dondangkyht != null)
            {
                if (dondangkyht.trangthaidon == 1)
                {
                    var authenticatedUser = _httpContextAccessor.HttpContext.User;//get data token
                    var phattuidClaim = authenticatedUser.FindFirst("phatuid");//tìm data token thằng nào là phattuid
                    if (phattuidClaim != null && int.TryParse(phattuidClaim.Value, out int phattuid))//check điều kiện và convert string sang int
                    {

                        dondangkyht.trangthaidon = 2;
                        dondangkyht.nguoixuly = phattuid;
                        dbContext.Update(dondangkyht);
                        dbContext.SaveChanges();
                        CapNhatThanhVien((int)dondangkyht.daotrangid);
                        return ErrorMessage.ThanhCong;
                    }
                }
                else if (dondangkyht.trangthaidon == 3)
                {
                    return ErrorMessage.TrangThaiDonDaDuocXoa;
                }
            }
            else
            {
                return ErrorMessage.DonDangKyKhongTonTai;
            }
            return ErrorMessage.DonDangKyKhongTonTai;
        }
        //từ chối đơn
        [Authorize(Roles = "admin")]
        public ErrorMessage rejectDonDangKy(int dondangkyid)
        {
            var dondangkyht = dbContext.Dondangkys.FirstOrDefault(x => x.dondangkyid == dondangkyid);
            if (dondangkyht != null)
            {
                if (dondangkyht.trangthaidon == 1)
                {
                    var authenticatedUser = _httpContextAccessor.HttpContext.User;
                    var phattuidClaim = authenticatedUser.FindFirst("phatuid");
                    if (phattuidClaim != null && int.TryParse(phattuidClaim.Value, out int phattuid))
                    {

                        dondangkyht.trangthaidon = 3;
                        dondangkyht.nguoixuly = phattuid;
                        dbContext.Update(dondangkyht);
                        dbContext.SaveChanges();
                        return ErrorMessage.ThanhCong;
                    }
                }
                else if (dondangkyht.trangthaidon == 2)
                {
                    return ErrorMessage.TrangThaiDonDaDuocDuyet;
                }
            }
            else
            {
                return ErrorMessage.DonDangKyKhongTonTai;
            }
            return ErrorMessage.DonDangKyKhongTonTai;
        }
    }
}
