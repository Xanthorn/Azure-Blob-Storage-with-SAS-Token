namespace Exercise1.DataLayer.Models
{
    public class User
    {
        public User() { }

        public User(int id, string email, string firstName, string? lastName, int? age, string? avatarName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            AvatarName = avatarName;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? AvatarName { get; set; }
    }
}
