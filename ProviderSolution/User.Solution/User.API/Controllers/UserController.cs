using Microsoft.AspNetCore.Mvc;
using User.API.Dto;
using User.Postgre.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/user/1
        [HttpGet("{userId}")]
        public ActionResult<UserDto> GetUserById([FromRoute]int userId)
        {
            var user = _repository.GetUserById(userid);
            return Ok(new UserDto
            {
                Id = user.Id,
                UserAddress = user.Address,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName
            });
        }
    }
}
