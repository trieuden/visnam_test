using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace be.Models
{
    public class User
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string passwordHash { get; set; } = string.Empty;

        public required string roleId { get; set; }

        [ForeignKey("roleId")]
        public Role role { get; set; } = null!;
    }
}