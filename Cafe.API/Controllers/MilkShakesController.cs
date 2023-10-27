using Cafe.API.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Cafe.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilkShakesController : ControllerBase
    {
        private readonly IMilkShakeRepository _milkShakeRepository;

        public MilkShakesController(IMilkShakeRepository milkShakeRepository)
        {
            _milkShakeRepository = milkShakeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMilkShakes()
        {
            try
            {
                var milkShakes = await _milkShakeRepository.GetMilkShakesAsync();
                return Ok(milkShakes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetMilkShake(int id)
        {
            try
            {
                var milkShake = await _milkShakeRepository.GetMilkShakeAsync(id);
                if (milkShake == null)
                {
                    return NotFound();
                }
                return Ok(milkShake);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutMilkShake(int id, MilkShake milkShake)
        {
            try
            {
                await _milkShakeRepository.UpdateMilkShakeAsync(id, milkShake);
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
        public async Task<IActionResult> PostMilkShake([FromBody] MilkShake milkShake)
        {
            try
            {
                await _milkShakeRepository.CreateMilkShakeAsync(milkShake);
                return CreatedAtAction("GetMilkShake", new { id = milkShake.Mid }, milkShake);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMilkShake(int id)
        {
            try
            {
                await _milkShakeRepository.DeleteMilkShakeAsync(id);
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