using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Api.Mappers;
using StoreBackend.Api.Models.Requests;
using StoreBackend.Exceptions;
using StoreBackend.Facade;

namespace StoreBackend.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserFacade userFacade;

        public UserController(IUserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        // GET api/users  → listar
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userFacade.GetAllAsync();
            var models = UserMapper.ToModel(users);
            return Ok(models);
        }

        // GET api/users/{id}  → detalle
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await userFacade.GetByIdAsync(id);
                var model = UserMapper.ToModel(user);
                return Ok(model);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/users  → crear
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel user)
        {
            try
            {
                var dto = UserMapper.ToDto(user);
                var createdUser = await userFacade.CreateAsync(dto);
                var model = UserMapper.ToModel(createdUser);
                return CreatedAtAction(nameof(GetUser), new { id = model.UserResourceId }, model);
            }
            catch (BadRequestResponseException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/users/{id}  → eliminar
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await userFacade.DeleteAsync(id);
                return Ok();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }
    }
}