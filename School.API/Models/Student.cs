namespace School.API.Models
{
    public class Student : Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Guid ClassID { get; set; }
    }
}
