using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class UserTicket
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string email { get; set; }

        public string token { get; set; }

        public DateTime issuedAt { get; set; }
    }
}
