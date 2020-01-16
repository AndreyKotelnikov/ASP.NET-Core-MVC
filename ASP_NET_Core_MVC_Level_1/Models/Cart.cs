using System.Collections.Generic;
using System.Linq;

namespace ASP_NET_Core_MVC.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemsCount => Items?.Sum(i => i.Quantity) 
                                 ?? 0;
    }
}