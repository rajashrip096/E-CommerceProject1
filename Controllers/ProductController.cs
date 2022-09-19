using E_CommerceProject1.DAL;
using E_CommerceProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceProject1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration configuration;
        ProductDAL productdal;
        
        CartDAL cd = new CartDAL();
        public ProductController(IConfiguration configuration)
        {
            this.configuration = configuration;
            productdal = new ProductDAL(configuration);
        }
        public IActionResult Index()
        {
            var model = productdal.GetAllProducts();
            return View(model);
        }

        public IActionResult AddProductToCart(int id)
        {
            string userid = HttpContext.Session.GetString("userid");
            Cart cart = new Cart();
            cart.ProductId = id;
            cart.UserId = Convert.ToInt32(userid);
            int res = cd.AddToCart(cart);
            if (res == 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult ViewCart()
        {
            string userid = HttpContext.Session.GetString("userid");
            var model = cd.ViewProductsFromCart(userid);
            return View(model);
        }

        [HttpGet]
        public IActionResult RemoveFromCart(int cid)
        {
            int res = cd.RemoveFromCart(cid);
            if (res == 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View();
            }
        }
        
    }
}
