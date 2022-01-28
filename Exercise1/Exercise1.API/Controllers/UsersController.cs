using Exercise1.API.Contracts;
using Exercise1.API.Contracts.User.Add;
using Exercise1.API.Contracts.User.Edit;
using Exercise1.DataLayer.Models;
using Exercise1.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Exercise1.API.Controllers
{
    [ApiController]
    public class UsersController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(ApiRoutes.User.GetAll)]
        public List<User> GetUsers() => _userRepository.GetUsers();

        [HttpGet(ApiRoutes.User.GetById)]
        public User GetUserById([FromRoute] int id) => _userRepository.GetUser(id);

        [HttpPost(ApiRoutes.User.Add)]
        public int AddUser([FromBody] AddUserRequest request)
        {
            User user = new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                AvatarUrl = request.AvatarUrl
            };
            return _userRepository.AddUser(user);
        }

        [HttpPut(ApiRoutes.User.Update)]
        public int UpdateUser([FromRoute] int id, [FromBody] EditUserRequest request)
        {
            User user = new()
            {
                Id = id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                AvatarUrl = request.AvatarUrl
            };
            return _userRepository.UpdateUser(user);
        }
    }
}
