using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leveir.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool RoleId { get; set; }
    }
}