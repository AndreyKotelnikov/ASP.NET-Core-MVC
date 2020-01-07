using System.Collections.Generic;
using System.Linq;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public SectionsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke() => View(GetSectionViewModels());

        private IEnumerable<SectionViewModel> GetSectionViewModels()
        {
            var sectionsWithoutParent = _productData.GetSections()
                .Where(s => s.ParentId is null)
                .ToArray();

            var sectionViewModels = sectionsWithoutParent.Select(s =>
                new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order,
                    ParentSection = null
                })
                .ToList();

            foreach (var sectionViewModel in sectionViewModels)
            {
                var childSections = _productData.GetSections()
                    .Where(s => s.ParentId == sectionViewModel.Id)
                    .ToArray();

                foreach (var childSection in childSections)
                {
                    sectionViewModel.ChildSections.Add(new SectionViewModel
                    {
                        Id = childSection.Id,
                        Name = childSection.Name,
                        Order = childSection.Order,
                        ParentSection = sectionViewModel
                    });
                }
                sectionViewModel.ChildSections.Sort((x, y) => Comparer<int>.Default.Compare(x.Order, y.Order));
            }
            
            sectionViewModels.Sort((x, y) => Comparer<int>.Default.Compare(x.Order, y.Order));

            return sectionViewModels;
        }
    }
}
