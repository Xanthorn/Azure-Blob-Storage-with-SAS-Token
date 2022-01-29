using Exercise1.API.Contracts;
using Exercise1.API.Contracts.User.Add;
using Exercise1.API.Contracts.User.Edit;
using Exercise1.DataLayer.Azure.StorageAccount;
using Exercise1.DataLayer.Models;
using Exercise1.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Exercise1.API.Controllers
{
    [ApiController]
    public class UsersController
    {
        private readonly IUserRepository _userRepository;
        private readonly IBlobContainerService _blobContainerService;

        public UsersController(IUserRepository userRepository, IBlobContainerService blobContainerService)
        {
            _userRepository = userRepository;
            _blobContainerService = blobContainerService;
        }

        [HttpGet(ApiRoutes.User.GetAll)]
        public List<User> GetUsers() => _userRepository.GetUsers();

        [HttpGet(ApiRoutes.User.GetById)]
        public User GetUserById([FromRoute] int id)
        {
            User user = _userRepository.GetUser(id);

            if (user.AvatarName != null)
            {
                user.AvatarName = _blobContainerService.GetImageUri(user.AvatarName);
            }

            return user;
        }

        [HttpPost(ApiRoutes.User.Add)]
        public async Task<int> AddUser([FromForm] AddUserRequest request)
        {
            string avatarName = null;
            if (request.Avatar != null)
            {
                avatarName = await _blobContainerService.UploadImage(request.Avatar);
            }

            User user = new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                AvatarName = avatarName
            };
            return _userRepository.AddUser(user);
        }

        [HttpPut(ApiRoutes.User.Update)]
        public async Task<int> UpdateUser([FromRoute] int id, [FromForm] EditUserRequest request)
        {
            User user = _userRepository.GetUser(id);

            string newAvatarName = null;
            if (request.Avatar != null)
            {
                newAvatarName = await _blobContainerService.UpdateImage(request.Avatar, user.AvatarName);
            }

            user = new()
            {
                Id = id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                AvatarName = newAvatarName
            };
            return _userRepository.UpdateUser(user);
        }
    }
}
