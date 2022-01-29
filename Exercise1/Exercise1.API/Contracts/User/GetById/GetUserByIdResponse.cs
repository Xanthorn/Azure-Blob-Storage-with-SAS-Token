namespace Exercise1.API.Contracts.User.GetById
{
    public class GetUserByIdResponse
    {
        public GetUserByIdResponse() { }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
