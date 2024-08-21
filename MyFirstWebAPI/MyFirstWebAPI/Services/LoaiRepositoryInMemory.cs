using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstWebAPI.Services
{
    public class LoaiRepositoryInMemory : ILoaiRepository
    {
        static List<LoaiVM> loaiVMs = new List<LoaiVM>
        {
            new LoaiVM {MaLoai = 1, TenLoai = "TV"},
            new LoaiVM {MaLoai = 2, TenLoai = "Tu Lanh"},
            new LoaiVM {MaLoai = 3, TenLoai = "Dieu Hoa"},
            new LoaiVM {MaLoai = 4, TenLoai = "May Giat"},
        };

        public LoaiVM Add(LoaiModel loai)
        {
            var _loai = new LoaiVM
            {
                MaLoai = loaiVMs.Max(lo => lo.MaLoai) + 1,
                TenLoai = loai.TenLoai
            };

            loaiVMs.Add( _loai );
            return _loai;
        }

        public void Delete(int id)
        {
            var _loai = loaiVMs.SingleOrDefault(lo => lo.MaLoai == id);
            if (_loai != null)
            {
                loaiVMs.Remove(_loai);
            }
        }

        public List<LoaiVM> GetAll()
        {
            return loaiVMs;
        }

        public LoaiVM GetById(int id)
        {
            return loaiVMs.SingleOrDefault(lo => lo.MaLoai == id);
        }

        public void Update(LoaiVM loai)
        {
            var _loai = loaiVMs.SingleOrDefault(lo => lo.MaLoai == loai.MaLoai);
            if ( _loai != null )
            {
                _loai.TenLoai = loai.TenLoai;
            }
        }
    }
}
