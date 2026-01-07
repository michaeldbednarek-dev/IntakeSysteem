using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntakeSysteemBack.Models
{
    public class Login
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class AuthenticatedResponse
    {
        public string Token { get; set; }
    }
}
