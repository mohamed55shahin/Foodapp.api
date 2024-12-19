﻿using FoodApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class Person:BaseEntity
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MyProperty { get; set; }
        public Role Role { get; set; }
    }
}