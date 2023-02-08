using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace slightly_sober.Models
{
    public class Login
    {
        [Key, Required]
        public int LoginID { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [SetsRequiredMembers]
        public Login(string passwordHash)
        {
            PasswordHash = passwordHash;
            IsActive = true;
        }
    }
}
