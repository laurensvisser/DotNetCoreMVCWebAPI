﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreMVCWebAPI.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        [MinLength(5)]
        public string FirstName { get; set; }
        [MinLength(7)]
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public ICollection<Order> Order { get; set; }
    }
}
