using System.ComponentModel.DataAnnotations;

namespace BaiThucTaplan1.Entity
{
    public class RolllNumber
    {
        [Key] public int rollnumber { get; set; }
        public int phantuid { get; set; }
        public Phantu Phantu { get; set; }
        public string roll { get; set; }
    }
}
