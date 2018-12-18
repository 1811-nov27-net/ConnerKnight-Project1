using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class ModelIngredient
    {
        public int IngredientId { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
