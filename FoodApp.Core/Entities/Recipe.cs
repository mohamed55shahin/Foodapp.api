using FoodApp.Api.FoodApp.Core.Entities;
using FoodApp.Core.Enums;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class Recipe : BaseEntity
    {
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal price { get; set; }  
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<RecipeTag> Tags { get; set; } = new List<RecipeTag>();

        public Item Item { get; set; }
    }
}
