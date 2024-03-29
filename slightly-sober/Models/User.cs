﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace slightly_sober.Models
{
    public class User
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required, StringLength(30)]
        public string Username { get; set; }

        [Required, StringLength(30)]
        [RegularExpression(@"^[A-Z][a-z ]*$", ErrorMessage = "First name must start with a capital letter, maximum 30 characters")]
        public string FirstName { get; set; }

        [Required, StringLength(30)]
        [RegularExpression(@"^[A-Z][a-z ]*$", ErrorMessage = "Last name must start with a capital letter, maximum 30 characters")]
        public string LastName { get; set; }

        [Required, StringLength(100)]
        [EmailAddress(ErrorMessage = "Email must be in a valid format")]
        public string Email { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        [ForeignKey("Login")]
        public int LoginID { get; set; }
        public virtual Login Login { get; set; }

        public virtual List<Cocktail>? Cocktails { get; set; } = null;

        [SetsRequiredMembers]
        public User(string username, string firstName, string lastName, string email, bool isAdmin)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsAdmin = isAdmin;
        }

        [SetsRequiredMembers]
        public User(string username, string firstName, string lastName, string email, bool isAdmin, Login login)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsAdmin = isAdmin;
            Login = login;
        }
    }
}
