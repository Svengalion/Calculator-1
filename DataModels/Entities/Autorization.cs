using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Entities
{
    public class Autorization
    {
        
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsConfirmed { get; set; }
        public bool RememberMe { get; set; }


        public override string ToString()
        {
            return UserId + ";" + Password + ";" + Email + ";" + IsConfirmed + ";" + RememberMe;
        }
    }
}
