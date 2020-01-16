using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.Models;
using ASP_NET_Core_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Domain.Entities;

namespace ASP_NET_Core_MVC.Infrastructure.Services
{
    public class CookieCartService : ICartService
    {
        private readonly IProductData _productData;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookIes = context.Response.Cookies;
                var cartCookie = context.Request.Cookies[_cartName];

                if (cartCookie is null)
                {
                    var cart = new Cart();
                    cookIes.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }
                ReplaceCookie(cookIes, cartCookie);
                return JsonConvert.DeserializeObject<Cart>(cartCookie);
            }

            set => ReplaceCookie(_httpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookie(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookie, new CookieOptions{Expires = DateTime.Now.AddDays(15)});
        }

        public CookieCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _productData = productData;
            _httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User;
            var userName = user.Identity.IsAuthenticated 
                ? user.Identity.Name 
                : null;
            _cartName = $"cart[{userName}]";
        }

        public void AddToCart(int id)
        {
            var cart = Cart;

            var item = cart.Items.SingleOrDefault(i => i.ProductId == id);
            if (item is null)
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = id, 
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;

            var item = cart.Items.SingleOrDefault(i => i.ProductId == id);

            if (item?.Quantity > 1)
            {
                item.Quantity--;
                Cart = cart;
            }
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;

            var item = cart.Items.SingleOrDefault(i => i.ProductId == id);
            if (item is null)
            {
                return;
            }

            cart.Items.Remove(item);

            Cart = cart;
        }

        public void RemoveAll()
        {
            var cart = Cart;

            cart.Items.Clear();

            Cart = cart;
        }

        public CartViewModel ConvertToCartViewModel()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                ProductIdList = Cart.Items.Select(i => i.ProductId).ToList()
            });


            var productViewModels = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand?.Name,
                ImageUrl = p.ImageUrl,
                Order = p.Order,
                Price = p.Price
            });

            return new CartViewModel
            {
                Items = Cart.Items.ToDictionary(
                    i => productViewModels.Single(p => p.Id == i.ProductId), 
                    i => i.Quantity)
            };
        }
    }
}
