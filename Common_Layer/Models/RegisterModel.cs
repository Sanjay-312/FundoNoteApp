using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class RegisterModel
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set;}
    }
}
