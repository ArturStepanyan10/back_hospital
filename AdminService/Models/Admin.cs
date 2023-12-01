namespace AdminService.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int UserId { get; set; }
    }
}
