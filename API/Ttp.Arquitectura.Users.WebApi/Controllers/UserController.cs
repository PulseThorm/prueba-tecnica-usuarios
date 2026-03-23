using Mapster;
using Microsoft.AspNetCore.Mvc;
using Ttp.Arquitectura.Users.Application.Commands;
using Ttp.Arquitectura.Users.Application.Queries;
using Ttp.Arquitectura.Users.WebApi.Models.Request;
using Ttp.Arquitectura.Users.WebApi.Models.Response;

namespace Ttp.Arquitectura.Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
        AddUserHandler addUserHandler, 
        GetUsersHandler getUsersHandler,
        DeleteUserHandler deleteUserHandler,
        UpdateUserHandler updateUserHandler) : ControllerBase
    {
        private readonly AddUserHandler _addUserHandler = addUserHandler;
        private readonly GetUsersHandler _getUsersHandler = getUsersHandler;
        private readonly DeleteUserHandler _deleteUserHandler = deleteUserHandler;
        private readonly UpdateUserHandler _updateUserHandler = updateUserHandler;
        
        [HttpGet]
        public IActionResult Get(int page = 1, int pageSize = 10)
        {
            return Ok(_getUsersHandler.Handle(page, pageSize)
                .Adapt<List<GetUserResponse>>());
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddUserRequest request)
        {
            _addUserHandler.Handle(request.Adapt<AddUserCommand>());
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _deleteUserHandler.Handle(new DeleteUserCommand { Id = id });
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateUserRequest request)
        {
            var command = request.Adapt<UpdateUserCommand>();
            command.Id = id;

            _updateUserHandler.Handle(command);

            return Ok();
        }
    }
}
