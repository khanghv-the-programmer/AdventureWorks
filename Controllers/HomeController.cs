﻿using AdventureWorksBeta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksBeta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AdventureWorks2014Context _db;
        int totalRecords = 0;
        int totalRecordsProducts = 0;
        int pageSize = 20;
        int totalPage = 0;
        public HomeController(ILogger<HomeController> logger, AdventureWorks2014Context db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(int? productPage)
        {
            List<Product> productList = _db.Product.ToList();
            List<Product> currentProList = new List<Product>();
            int currentPage = 0;
            totalRecordsProducts = productList.Count;
            if (totalRecordsProducts % pageSize == 0)
            {
                totalPage = totalRecordsProducts / pageSize;
            }
            else
            {
                totalPage = totalRecordsProducts / pageSize + 1;
            }
            if(productPage == null)
            {
                productPage = default(int);
            }
            else
            {
                currentPage = productPage.GetValueOrDefault();


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


            for (int i = currentPage * pageSize; i < currentPage * pageSize + pageSize - 1; i++)
            {
                if (i >= totalRecordsProducts)
                {
                    break;
                }
                currentProList.Add(productList.ElementAt(i));
            }

            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = currentPage;

            return View(currentProList);
        }

        [HttpGet]
        public IActionResult People(int? getPage)
        {
            List<Person> people = _db.Person.ToList();

            List<Person> currentList = new List<Person>();
            int currentPage = 0;
            totalRecords = people.Count;
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
            for (int i = currentPage * pageSize; i < currentPage * pageSize + pageSize - 1; i++)
            {
                if (i >= totalRecords)
                {
                    break;
                }
                currentList.Add(people.ElementAt(i));
            }
            
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = currentPage;

            return View(currentList);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProductDetail(int? pId)
        {
            var productdetail = _db.Product.FirstOrDefault(pro => pro.ProductId == pId);
            return View(productdetail);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
