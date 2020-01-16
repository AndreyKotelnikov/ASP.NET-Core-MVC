using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC.Components
{
    public class FeaturesItemsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public FeaturesItemsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke() => View(GetProductViewModels());

        private IEnumerable<ProductViewModel> GetProductViewModels()
        {
            var productViewModels = _productData.GetProducts()
                .Take(6)
                .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand?.Name,
                Order = p.Order,
                Price = p.Price
            }).OrderBy(p => p.Order)
                .ToList();

            return productViewModels;
        }
    }
}
