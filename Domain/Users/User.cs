using Domain.Base;
using System;
using System.Collections.Generic;

namespace Domain.Users
{
    public class User : BaseEntity<int>
    {
        public User()
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}