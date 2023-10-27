using Cafe.API.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Cafe.Data.Models;

namespace Cafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeesController : ControllerBase
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public CoffeesController(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetCoffees()
        {
            try
            {
                var coffees = await _coffeeRepository.GetCoffeesAsync();
                return Ok(coffees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoffee(int id)
        {
            try
            {
                var coffee = await _coffeeRepository.GetCoffeeAsync(id);
                if (coffee == null)
                {
                    return NotFound();
                }
                return Ok(coffee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoffee(int id, Coffee coffee)
        {
            try
            {
                await _coffeeRepository.UpdateCoffeeAsync(id, coffee);
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
        public async Task<IActionResult> PostCoffee([FromBody] Coffee coffee)
        {
            try
            {
                await _coffeeRepository.CreateCoffeeAsync(coffee);
                return CreatedAtAction("GetCoffee", new { id = coffee.Coffeeid }, coffee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffee(int id)
        {
            try
            {
                await _coffeeRepository.DeleteCoffeeAsync(id);
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
    }
}

