using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC.Data;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using WebStore.Domain.Entities;

namespace ASP_NET_Core_MVC.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;
       
        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
