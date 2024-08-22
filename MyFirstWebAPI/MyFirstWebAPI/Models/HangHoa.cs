using System;

namespace MyFirstWebAPI.Models
{
    public class HangHoaModel
    {
        public string TenHangHoa { get; set; }
        public string MoTa {  get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }

    }

    public class HangHoa : HangHoaVM
    {
        public Guid MaHangHoa { get; set;}
    }

    public class HangHoaVM
    {
        public Guid MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string MoTa { get; set; }
        public double DonGia { get; set; }
        public string TenLoai { get; set; }
    }
}
