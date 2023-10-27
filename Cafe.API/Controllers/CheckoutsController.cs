using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cafe.API.IRepository;
using Cafe.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        private readonly ICheckoutRepository _checkoutRepository;

        public CheckoutsController(ICheckoutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCheckouts()
        {
            try
            {
                var checkouts = await _checkoutRepository.GetCheckoutsAsync();
                return Ok(checkouts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheckout(int id)
        {
            try
            {
                var checkout = await _checkoutRepository.GetCheckoutAsync(id);
                if (checkout == null)
                {
                    return NotFound();
                }
                return Ok(checkout);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckout(int id, [FromBody] Checkout checkout)
        {
            try
            {
                await _checkoutRepository.UpdateCheckoutAsync(id, checkout);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCheckout([FromBody] Checkout checkout)
        {
            try
            {
                await _checkoutRepository.CreateCheckoutAsync(checkout);
                return CreatedAtAction("GetCheckout", new { id = checkout.Chid }, checkout);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckout(int id)
        {
            try
            {
                await _checkoutRepository.DeleteCheckoutAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
