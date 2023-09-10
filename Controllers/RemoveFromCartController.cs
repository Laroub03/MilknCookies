using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;  

namespace MilknCookies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoveFromCartController : ControllerBase
    {
        [HttpDelete]
        public IActionResult Delete(string productName)
        {
            // Get the shopping cart from the session
            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData))
            {
                string cartJson = Encoding.UTF8.GetString(cartData);
                List<Product> shoppingCart = JsonConvert.DeserializeObject<List<Product>>(cartJson);

                // Remove the product with the specified name from the cart
                shoppingCart.RemoveAll(p => p.Name == productName);

                // Save the cart back to the session
                HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(shoppingCart));

                return Ok(shoppingCart);
            }

            return NotFound("Shopping cart not found.");
        }
    }
}