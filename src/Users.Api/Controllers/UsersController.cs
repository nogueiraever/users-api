using Microsoft.AspNetCore.Mvc;
using Users.Application.UseCases.Users.CreateUsers;
using Users.Application.UseCases.Users.GetUserById;

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

            return BadRequest(result);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserEmail(int userId, UpdateUserEmailCommand updateUserEmailCommand, IUpdateUserEmailUseCase updateUserEmailUseCase)
        {
            updateUserEmailCommand.UserId = userId;

            var result = await updateUserEmailUseCase.Handle(updateUserEmailCommand);
            if (result.IsSuccess)
                return Ok();

            return BadRequest(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(int userId, IGetUserByIdUseCase getUserByIdUseCase)
        {
            var result = await getUserByIdUseCase.Handle(userId);
            if (result.IsSuccess)
                return Ok(result);

            return NotFound();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveUserById(int userId, IRemoveUserByIdUseCase removeUserByIdUseCase)
        {
            var result = await removeUserByIdUseCase.Handle(userId);
            if (result.IsSuccess)
                return Ok(result);

            return NotFound();
        }
    }
}
