using Domain.Base;
using System;
using System.Collections.Generic;

namespace Domain.Providers
{
    public class Provider : BaseEntity<int>
    {
        public Provider()
        {
        }
        public string NIT { get;  set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
    }
}