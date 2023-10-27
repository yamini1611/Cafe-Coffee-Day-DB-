using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cafe.API.IRepository;
using Cafe.Data.Models;
using MimeKit;
using Cafe.Models;
using MailKit.Net.Smtp;

namespace Cafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            try
            {
                var carts = await _cartRepository.GetCartsAsync();
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(int id)
        {
            try
            {
                var cart = await _cartRepository.GetCartAsync(id);
                if (cart == null)
                {
                    return NotFound();
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart updatedCart)
        {
            try
            {
                await _cartRepository.UpdateCartAsync(id, updatedCart);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCart([FromBody] Cart cart)
        {
            try
            {
                await _cartRepository.CreateCartAsync(cart);
                return CreatedAtAction("GetCart", new { id = cart.Cid }, cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("Delete{cartId}")]
        public IActionResult DeleteCartItem(int cartId)
        {
            try
            {
                _cartRepository.DeleteCartItem(cartId);
                return Ok("Item deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> DeleteCart(int userid)
        {
            try
            {
                await _cartRepository.DeleteCartAsync(userid);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequest emailRequest)
        {
            if (ModelState.IsValid)
            {

                if (emailRequest == null)
                {
                    return BadRequest("Invalid request");
                }

                try
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse("20bsca151yaminipriyaj@skacas.ac.in"));
                    message.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
                    message.Subject = "Order Confirmation";

                    var text = new TextPart("plain")
                    {
                        Text = $"Dear {emailRequest.Uname} ," +"\n"+

                                $"Thankyou for placing an order for {emailRequest.Product} with a total of {emailRequest.TotalPrice}"
                    };

                    var multipart = new Multipart("mixed")
                    {
                    text
                    };
                    message.Body = multipart;

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, false);
                        await client.AuthenticateAsync("20bsca151yaminipriyaj@skacas.ac.in", "Yamini@1611");
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }

                    return Ok("Email sent successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return Content("Send Email method executed");
        }
    }
}
