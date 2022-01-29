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
        public string NIT { get; internal set; }
        public string Name { get; internal set; }
        public string Email { get; internal set; }
    }
}