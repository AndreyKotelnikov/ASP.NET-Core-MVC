using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke() => View(GetBrandViewModels());

        private IEnumerable<BrandViewModel> GetBrandViewModels()
        {
            var brandViewModels = _productData.GetBrands()
                .Select(b => new BrandViewModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Order = b.Order
                    })
                .OrderBy(b => b.Order)
                .ToList();

            return brandViewModels;
        }
    }
}
