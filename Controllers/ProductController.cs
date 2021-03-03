using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksBeta.Models;
using Microsoft.Extensions.Logging;

namespace AdventureWorksBeta.Controllers
{
    public class ProductController : Controller
    {

        private readonly ILogger<ProductController> _logger;
        private AdventureWorks2014Context _db;
        int totalRecords = 0;
        int pageSize = 20;
        int totalPage = 0;

        public ProductController(ILogger<ProductController> logger, AdventureWorks2014Context db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult CreateProduct()
        {

            List<UnitMeasure> weightList = _db.UnitMeasure.ToList();
            List<UnitMeasure> sizeList = _db.UnitMeasure.ToList();
            List<ProductSubcategory> subCate = _db.ProductSubcategory.ToList();
            List<ProductModel> modelList = _db.ProductModel.ToList();

            ViewBag.SizeUnitMeasureCode = sizeList;
            ViewBag.WeightUnitMeasureCode = weightList;
            ViewBag.ProductSubcategoryId = subCate;
            ViewBag.ProductModelId = modelList;
            return View();
        }
        public IActionResult SearchProductByCategoryAndSubCategory(string idSubString, int? getPage)
        {
            int idSub = 0;
            try
            {
                idSub = Convert.ToInt32(idSubString);
            }
            catch (Exception)
            {

                idSub = 0;
            }

            List<Product> products = _db.Product.Where(products => products.ProductSubcategoryId == idSub).ToList();

            List<Product> currentList = new List<Product>();
            int currentPage = 0;
            totalRecords = products.Count;
            if (totalRecords % pageSize == 0)
            {
                totalPage = totalRecords / pageSize;
            }
            else
            {
                totalPage = totalRecords / pageSize + 1;
            }

            if (getPage == null)
            {
                currentPage = default(int);
            }
            else
            {
                currentPage = getPage.GetValueOrDefault();


            }
            if (currentPage > totalPage)
                currentPage = 0;
            try
            {
                Convert.ToInt32(currentPage);
            }
            catch (Exception)
            {

                currentPage = 0;
            }
            for (int i = currentPage * pageSize; i < currentPage * pageSize + pageSize; i++)
            {
                if (i >= totalRecords)
                {
                    break;
                }
                currentList.Add(products.ElementAt(i));
            }
            string nameSub = "Not found";
            nameSub = _db.ProductSubcategory.FirstOrDefault(ProductSubcategory => ProductSubcategory.ProductSubcategoryId == idSub).Name;
            ViewBag.NameSub = nameSub;
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = currentPage;
            ViewBag.IdSub = idSubString;

            
            
            return View(currentList);
        }

        public IActionResult AddProduct()
        {
            return View();
        }
    }
}
