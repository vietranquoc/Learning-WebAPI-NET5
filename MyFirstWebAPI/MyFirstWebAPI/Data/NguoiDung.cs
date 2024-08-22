using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebAPI.Data
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(3, 50)]
        public string UserName { get; set; }

        [Required]
        [Range(8, 250)]
        public string Password { get; set; }

        public string HoTen { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

    }
}
