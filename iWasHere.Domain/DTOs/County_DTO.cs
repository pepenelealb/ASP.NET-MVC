using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class County_DTO
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int CountryId { get; set; }

        private static bool UpdateDatabase = false;
        private DictionaryCounty entities;

        //public ProductService(DictionaryCounty entities)
        //{
        //    this.entities = entities;
        //}
        //public IList<DictionaryCity> GetAll()
        //{
        //    var result = HttpContext.Current.Session["Products"] as IList<ProductViewModel>;

        //    if (result == null || UpdateDatabase)
        //    {
        //        result = entities.DictionaryCity.Select(city => new ProductViewModel
        //        {
        //            CityID = city.CityID,
        //            ProductName = product.ProductName,
        //            UnitPrice = product.UnitPrice.HasValue ? product.UnitPrice.Value : default(decimal),
        //            UnitsInStock = product.UnitsInStock.HasValue ? product.UnitsInStock.Value : default(short),
        //            QuantityPerUnit = product.QuantityPerUnit,
        //            Discontinued = product.Discontinued,
        //            UnitsOnOrder = product.UnitsOnOrder.HasValue ? (int)product.UnitsOnOrder.Value : default(int),
        //            CategoryID = product.CategoryID,
        //            Category = new CategoryViewModel()
        //            {
        //                CategoryID = product.Category.CategoryID,
        //                CategoryName = product.Category.CategoryName
        //            },
        //            LastSupply = DateTime.Today
        //        }).ToList();

        //        HttpContext.Current.Session["Products"] = result;
        //    }

        //    return result;
        //}
    }

}
