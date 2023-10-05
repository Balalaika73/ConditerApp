using System;
using System.Collections.Generic;

namespace Session2.Models
{
    public partial class User
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Fio { get; set; }
        public string? Photo { get; set; }
    }
}
