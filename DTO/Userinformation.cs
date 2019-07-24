using System;

namespace Alliance_for_Life.Models
{
    public class Userinformation
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Organization { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}