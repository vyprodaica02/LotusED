using BaiThucTaplan1.ConvertDTO;
using BaiThucTaplan1.DTO;
using BaiThucTaplan1.Entity;
using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.pagination;
using Microsoft.EntityFrameworkCore;
using System;

namespace BaiThucTaplan1.Services
{
    public class DaoTrangServices : IDaoTrang
    {
        private readonly appDbcontext dbContext;
        private readonly DaoTrangDTOConvert daoTrangDTOConvert;
        public DaoTrangServices()
        {
            this.dbContext = new appDbcontext();
            daoTrangDTOConvert = new DaoTrangDTOConvert();
        }



        public ErrorMessage SuaDaoTrang(DaoTrangDTO daoTrangDTO)
        {
            var daoTrangHT = dbContext.daoTrangs.FirstOrDefault(x => x.daotrangid == daoTrangDTO.daotrangid);
            if (daoTrangHT != null)
            {
                daoTrangHT.daotrangid = daoTrangDTO.daotrangid;
                daoTrangHT.daketthuc = daoTrangDTO.daketthuc;
                daoTrangHT.noidung = daoTrangDTO.noidung;
                daoTrangHT.noitochuc = daoTrangDTO.noitochuc;
                daoTrangHT.sothanhvienthamgia = daoTrangDTO.sothanhvienthamgia;
                daoTrangHT.thoigiantochuc = daoTrangDTO.thoigiantochuc;
                daoTrangHT.nguoitrutri = daoTrangDTO.nguoitrutri;
                dbContext.Update(daoTrangHT);
                dbContext.SaveChanges();
                return ErrorMessage.ThanhCong;
            }
            return ErrorMessage.ThatBai;
        }

        public ErrorMessage ThemDaoTrang(DaoTrang daoTrang)
        {
            daoTrang.thoigiantochuc = DateTime.Now.AddDays(10);
            daoTrang.daketthuc = false;
            dbContext.Add(daoTrang);
            dbContext.SaveChanges();
            return ErrorMessage.ThanhCong;
        }

        public ErrorMessage XoaDaoTrang(int idDaoTrang)
        {

            var dapTrangHT = dbContext.daoTrangs.FirstOrDefault(x => x.daotrangid == idDaoTrang);
            if (dapTrangHT != null)
            {
                var dondangkys = dbContext.Dondangkys.Where(x => x.daotrangid == idDaoTrang);

                dbContext.RemoveRange(dondangkys);
                dbContext.Remove(dapTrangHT);
                dbContext.SaveChanges();
                return ErrorMessage.ThanhCong;
            }
            return ErrorMessage.ThatBai;
        }


        public List<DaoTrangDTO> GetDaoTrang(string? noiToChuc, int pageSize, int pageNumber)
        {
            var query = dbContext.daoTrangs.AsQueryable();

            if (!string.IsNullOrEmpty(noiToChuc))
            {
                query = query.Where(x => x.noitochuc.ToLower().Contains(noiToChuc.ToLower()));
            }

            var result = query
                .OrderByDescending(x => x.noitochuc)
                .ToList();

            var pagingHelper = new PagingHelper<DaoTrangDTO>(pageSize, pageNumber);
            var pagedResult = pagingHelper.GetPagedResult(result.Select(x => daoTrangDTOConvert.EntityDaoTrangDTO(x)).ToList());

            return pagedResult;
        }

        public List<DaoTrangDTO> GetAllDaoTrang(string? noiToChuc)
        {
            var query = dbContext.daoTrangs.AsQueryable();

            if (!string.IsNullOrEmpty(noiToChuc))
            {
                query = query.Where(x => x.noitochuc.ToLower().Contains(noiToChuc.ToLower()));
            }
            var check = query.OrderByDescending(x => x.noitochuc).Select(x => daoTrangDTOConvert.EntityDaoTrangDTO(x)).ToList();
            return check;
        }
      

    }
}
