using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstWebAPI.Services
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly MyDbContext _context;

        //5 items per page
        public static int PAGE_SIZE { get; set; } = 5;

        public HangHoaRepository(MyDbContext context)
        {
            _context = context;
        }

        public HangHoaVM Add(HangHoaModel model)
        {
            var data = new Data.HangHoa
            {
                MaHh = Guid.NewGuid(),
                TenHh = model.TenHangHoa,
                MoTa = model.MoTa,
                DonGia = model.DonGia,
                GiamGia = model.GiamGia,
                MaLoai = model.MaLoai,
            };

            _context.Add(data);
            _context.SaveChanges();

            return new HangHoaVM
            {
                MaHangHoa = data.MaHh,
                TenHangHoa = data.TenHh,
                MoTa = data.MoTa,
                DonGia = data.DonGia,
                TenLoai = data.Loai?.TenLoai,
            };
        }

        public void DeleteById(string id)
        {
            var _hangHoa = _context.HangHoas.SingleOrDefault(hh => hh.MaHh == Guid.Parse(id));
            if (_hangHoa != null)
            {
                _context.Remove(_hangHoa);
                _context.SaveChanges();
            }
        }

        //Search by TenHangHoa
        public List<HangHoaVM> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProducts = _context.HangHoas
                            .Include(hh => hh.Loai)
                            .AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(hh => hh.TenHh.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.DonGia >=  from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.DonGia <= to);
            }
            #endregion

            #region Sorting
            //Defaul sort by Name (TenHh)
            allProducts = allProducts.OrderBy(hh => hh.TenHh);

            if (!string.IsNullOrEmpty(sortBy)) {
                switch(sortBy)
                {
                    case "tenhh_desc": 
                        allProducts = allProducts.OrderByDescending(hh => hh.TenHh);
                        break;
                    case "gia_asc":
                        allProducts = allProducts.OrderBy(hh => hh.DonGia); 
                        break;
                    case "gia_desc":
                        allProducts = allProducts.OrderByDescending(hh => hh.DonGia);
                        break;
                }
            }
            #endregion

            //#region Paging
            //allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            //#endregion

            ////convert entity HangHoa to HangHoaVM
            //var result = allProducts.Select(hh => new HangHoaVM
            //{
            //    MaHangHoa = hh.MaHh,
            //    TenHangHoa = hh.TenHh,
            //    MoTa = hh.MoTa,
            //    DonGia = hh.DonGia,
            //    TenLoai= hh.Loai.TenLoai,
            //});

            //return result.ToList();

            var result = PaginatedList<Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            return result.Select(hh => new HangHoaVM
            {
                MaHangHoa = hh.MaHh,
                TenHangHoa = hh.TenHh,
                MoTa = hh.MoTa,
                DonGia = hh.DonGia,
                TenLoai = hh.Loai?.TenLoai,
            }).ToList();
        }

        public HangHoaVM GetByid(string id)
        {
            var hangHoa = _context.HangHoas.Where(hh => hh.MaHh == Guid.Parse(id));
            if (hangHoa == null)
            {
                return null;
            } 
            else
            {
                var result = hangHoa.Select(hh => new HangHoaVM
                {
                    MaHangHoa = hh.MaHh,
                    TenHangHoa = hh.TenHh,
                    MoTa = hh.MoTa,
                    DonGia = hh.DonGia,
                    TenLoai = hh.Loai.TenLoai,
                });

                return result.FirstOrDefault();
            }
        }

        public void UpdateById(HangHoaVM hangHoa)
        {
            var _hangHoa = _context.HangHoas.SingleOrDefault(hh => hh.MaHh == hangHoa.MaHangHoa);
            if (hangHoa != null)
            {
                _hangHoa.TenHh = hangHoa.TenHangHoa;
                _hangHoa.DonGia = hangHoa.DonGia;

                _context.SaveChanges();
            }
        }
    }
}
