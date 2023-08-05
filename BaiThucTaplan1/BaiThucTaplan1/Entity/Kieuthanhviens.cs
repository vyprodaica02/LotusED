using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.Entity
{
    public class Kieuthanhviens
    {
        [Key] public int kieuthanhvienid { get; set; }
        public string? code { get; set; }
        public string? tenkieu { get; set; }

        public IEnumerable<Phantu>? phantus { get; set; }
    }
}
