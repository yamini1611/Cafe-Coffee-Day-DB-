using Cafe.API.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Cafe.Data.Models;

namespace Cafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EatablesController : ControllerBase
    {
        private readonly IEatableRepository _eatableRepository;

        public EatablesController(IEatableRepository eatableRepository)
        {
            _eatableRepository = eatableRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEatables()
        {
            try
            {
                var eatables = await _eatableRepository.GetEatablesAsync();
                return Ok(eatables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEatable(int id)
        {
            try
            {
                var eatable = await _eatableRepository.GetEatableAsync(id);
                if (eatable == null)
                {
                    return NotFound();
                }
                return Ok(eatable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEatable(int id, Eatable eatable)
        {
            try
            {
                await _eatableRepository.UpdateEatableAsync(id, eatable);
                return Ok();
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
        public async Task<IActionResult> PostEatable([FromBody] Eatable eatable)
        {
            try
            {
                await _eatableRepository.CreateEatableAsync(eatable);
                return CreatedAtAction("GetEatable", new { id = eatable.Eid }, eatable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEatable(int id)
        {
            try
            {
                await _eatableRepository.DeleteEatableAsync(id);
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