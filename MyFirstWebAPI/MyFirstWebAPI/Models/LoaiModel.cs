using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPI.Models
{
    public class LoaiModel
    {
        [Required]
        [MaxLength(50)]
        public string TenLoai {  get; set; }
    }

}
