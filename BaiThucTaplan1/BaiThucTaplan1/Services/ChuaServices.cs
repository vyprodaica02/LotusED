using BaiThucTaplan1.ConvertDTO;
using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.pagination;
using Microsoft.EntityFrameworkCore;

namespace BaiThucTaplan1.Services
{
    public class ChuaServices : Ichua
    {
        private readonly appDbcontext dbcontext;
        private readonly ChuaDTOConvert chuaDTOConvert;
        public ChuaServices()
        {
            this.dbcontext = new appDbcontext();
            chuaDTOConvert = new ChuaDTOConvert();
        }

        public List<ChuaDTO> GetAllChua(string? Ten)
        {
            var check = dbcontext.Chuas.OrderByDescending(x => x.tenchua).AsQueryable();
            if (!string.IsNullOrEmpty(Ten))
            {
                check = check.Where(x => x.tenchua.ToLower().Contains(Ten.ToLower())).AsQueryable();
            }
            var query = check.OrderByDescending(x=>x.tenchua).Select(x=> chuaDTOConvert.EntityChuaDTO(x)).ToList();
            return query;
        }

        public List<ChuaDTO> GetChua(string? Ten, int pageSize, int pageNumber)
        {
            var check = dbcontext.Chuas.OrderByDescending(x => x.tenchua).AsQueryable();
            if (!string.IsNullOrEmpty(Ten))
            {
                check = check.Where(x => x.tenchua.ToLower().Contains(Ten.ToLower())).AsQueryable();
            }

            var pagingHelper = new PagingHelper<ChuaDTO>(pageSize, pageNumber);
            var pagedResult = pagingHelper.GetPagedResult(check.Select(x => chuaDTOConvert.EntityChuaDTO(x)).ToList());
            return pagedResult;
        }

        public ErrorMessage SuaChua(Chuas chuas, int id)
        {
            var chuaHt = dbcontext.Chuas.FirstOrDefault(x => x.chuaid == id);
            if (chuaHt != null)
            {
                chuaHt.chuaid = chuas.chuaid;
                chuaHt.capnhat = chuas.capnhat;
                chuaHt.diachi = chuas.diachi;
                chuaHt.ngaythanhlap = chuas.ngaythanhlap;
                chuaHt.tenchua = chuas.tenchua;
                chuaHt.trutri = chuas.trutri;
                dbcontext.Update(chuaHt);
                dbcontext.SaveChanges();
                return ErrorMessage.ThanhCong;
            }
            return ErrorMessage.ThatBai;

        }

        public ErrorMessage ThemChua(Chuas chuas)
        {
            dbcontext.Add(chuas);
            dbcontext.SaveChanges();
            return ErrorMessage.ThanhCong;
        }

        public ErrorMessage XoaChua(int id)
        {
            var ChuaHt = dbcontext.Chuas.FirstOrDefault(x => x.chuaid == id);
            if (ChuaHt != null)
            {
                dbcontext.Remove(ChuaHt);
                dbcontext.SaveChanges();
                return ErrorMessage.ThanhCong;
            }
            return ErrorMessage.ThatBai;
        }
    }
}
