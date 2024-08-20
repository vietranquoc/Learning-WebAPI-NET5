using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPI.Models
{
    public class LoaiVM
    {
        [Required]
        [MaxLength(50)]
        public string TenLoai {  get; set; }
    }

}
