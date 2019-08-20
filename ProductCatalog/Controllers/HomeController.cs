using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProductCatalog.Models;
using System;
using System.Collections.Generic;

namespace ProductCatalog.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IProduct Product;
        private readonly IMemoryCache Cache;

        public HomeController(IProduct product, IMemoryCache memoryCache)
        {
            Product = product;
            Cache = memoryCache;
        }

        [Route("")]
        [Route("~/")]
        [Route("~/Home")]
        public IActionResult Products()
        {
            IEnumerable<Product> model = Product.GetProducts();

            return View(model);
        }

        [Route("{id?}")]
        public ViewResult Details(int? id)
        {
            Product model;

            if (Cache.Get(id) != null)
                model = Cache.Get<Product>(id ?? 1);
            else
            {
                model = Product.GetProduct(id ?? 1);

                if(id.HasValue)
                    CacheTryGetValueSet(id.Value);
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult AddProduct()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                if (Product.IsDuplicateName(product.Name))
                {
                    ModelState.AddModelError("", "This product already exists in the catalog.");
                    return View(product);
                }

                Product newProduct = Product.AddProduct(product);
                CacheTryGetValueSet(newProduct.Id);
                return RedirectToAction("Details", new { id = newProduct.Id });
            }

            return View();
        }

        public IActionResult CacheTryGetValueSet(int id)
        {
            Product cacheEntry;

            if (!Cache.TryGetValue<Product>(id, out cacheEntry))
            {
                cacheEntry = Product.GetProduct(id);

                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(120));

                Cache.Set(id, cacheEntry, cacheEntryOptions);
            }

            return View("Details", cacheEntry);
        }
    }
}
