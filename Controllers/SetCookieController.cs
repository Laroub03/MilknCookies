using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace MilknCookies.Controllers
{
    [Produces("application/json")]
    [Route("api/SetCookie")]
    public class SetCookieController : Controller
    {
        // GET: api/SetCookie
        [HttpGet]
        public IActionResult Get(string favoriteMilkshake)
        {
            // Create cookie options to set the expiration time to 5 minutes from now
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(5)
            };

            // Add the "favoriteMilkshake" value to a cookie with the specified options
            Response.Cookies.Append("favoriteMilkshake", favoriteMilkshake, cookieOptions);

            return Ok(favoriteMilkshake);
        }

        // GET: api/SetCookie/GetFromCookie
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetFromCookie()
        {
            // Check if the "favoriteMilkshake" cookie exists
            if (Request.Cookies["favoriteMilkshake"] != null)
            {
                // Retrieve the value of the "favoriteMilkshake" cookie
                var favoriteMilkshake = Request.Cookies["favoriteMilkshake"];

                return Ok(favoriteMilkshake);
            }
            else
            {
                return Ok("unknown");
            }
        }

        // GET: api/SetCookie/RemoveCookie
        [HttpGet]
        [Route("[action]")]
        public IActionResult RemoveCookie()
        {
            // Create cookie options to set the expiration time to the past, effectively removing the cookie
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            // Add an empty value to the "favoriteMilkshake" cookie with expired options to remove it
            Response.Cookies.Append("favoriteMilkshake", "", cookieOptions);

      
            return Ok("Cookie removed");
        }
    }
}
