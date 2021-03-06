﻿using WebStore.Domain.Entities.Base.Interfaces;

namespace ASP_NET_Core_MVC.ViewModels
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
