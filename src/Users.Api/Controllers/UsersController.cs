using Microsoft.AspNetCore.Mvc;
using Users.Application.UseCases.Users.CreateUsers;

namespace Users.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType<int>(StatusCodes.Status201Created)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand, ICreateUserUseCase createUserUseCase)
        {
            var result = await createUserUseCase.Handle(createUserCommand);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(CreateUser), new { id = result.Value }, result.Value);

            return BadRequest();
        }
    }
}
