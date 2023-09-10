using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace MilknCookies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string productName, decimal price)
        {
            List<Product> shoppingCart;

            // Check if the shopping cart already exists in the session
            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData))
            {
                // If the cart exists, retrieve it from the session
                string cartJson = Encoding.UTF8.GetString(cartData);
                shoppingCart = JsonConvert.DeserializeObject<List<Product>>(cartJson);
            }
            else
            {
                // If the cart doesn't exist, create a new empty cart
                shoppingCart = new List<Product>();
            }

            // Add the new product to the cart
            shoppingCart.Add(new Product { Name = productName, Price = price });

            // Save the cart back to the session
            HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(shoppingCart));

            return Ok(shoppingCart);
        }
    }
}
