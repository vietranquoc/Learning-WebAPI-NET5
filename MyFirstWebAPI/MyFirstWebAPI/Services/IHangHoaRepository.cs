using MyFirstWebAPI.Models;
using System.Collections.Generic;

namespace MyFirstWebAPI.Services
{
    public interface IHangHoaRepository
    {
        List<HangHoaVM> GetAll(string search, double? from, double? to, string sortBy, int page = 1);
        HangHoaVM GetByid(string id);
        HangHoaVM Add(HangHoaModel hangHoa);
        void UpdateById(HangHoaVM hangHoa);
        void DeleteById(string id);
    }
}
