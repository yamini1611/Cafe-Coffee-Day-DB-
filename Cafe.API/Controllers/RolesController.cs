using Microsoft.AspNetCore.Mvc;
using Cafe.Data.Models; 
using Cafe.API.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace Cafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                List<Role> roles = (await _roleRepository.GetRolesAsync()).ToList();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            try
            {
                var role = await _roleRepository.GetRoleAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutRole(int id, [FromBody] Role role)
        {
            try
            {
                await _roleRepository.UpdateRoleAsync(id, role);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PostRole([FromBody] Role role)
        {
            try
            {
                await _roleRepository.CreateRoleAsync(role);
                return CreatedAtAction("GetRole", new { id = role.Roleid }, role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                await _roleRepository.DeleteRoleAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}