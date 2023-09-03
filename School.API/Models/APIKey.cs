using Microsoft.EntityFrameworkCore;

namespace School.API.Models
{
    public class APIKey
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string UserRole { get; set; }

        public Guid Key { get; set; }
    }
}
