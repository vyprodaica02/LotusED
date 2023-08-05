using Microsoft.EntityFrameworkCore;

namespace BaiThucTaplan1.Entity
{
    public class appDbcontext : DbContext
    {
        public DbSet<Chuas> Chuas { get; set; }
        public DbSet<DaoTrang> daoTrangs { get; set; }
        public DbSet<Dondangkys> Dondangkys { get; set; }
        public DbSet<Kieuthanhviens> Kieuthanhviens { get; set; }
        public DbSet<Phantu> Phantus { get; set; }
        public DbSet<Phantudaotrangs> Phantudaotrangs { get; set; }
        public DbSet<RolllNumber>  rolllNumbers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server = DESKTOP-BG2A1PJ; database = BaiThucTaplan1; trusted_Connection = true;TrustServerCertificate=True;");
        }
    }
}
