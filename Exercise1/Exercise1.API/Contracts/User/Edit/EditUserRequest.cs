namespace Exercise1.API.Contracts.User.Edit
{
    public class EditUserRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
