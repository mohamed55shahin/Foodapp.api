using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class User:Person
    {
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
