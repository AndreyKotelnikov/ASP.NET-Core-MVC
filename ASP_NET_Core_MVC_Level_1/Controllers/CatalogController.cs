using System.Linq;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;

namespace ASP_NET_Core_MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Shop(int? brandId, int? sectionId)
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId
            });

            var catalog = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Select(p =>
                        new ProductViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Order = p.Order,
                            ImageUrl = p.ImageUrl,
                            Price = p.Price
                        })
                    .OrderBy(p => p.Order)
            };
            
            return View(catalog);
        }

        public IActionResult Detailes(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }
            
            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Brand = product.Brand?.Name
            });
        }
    }
}