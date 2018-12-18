using Project0.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class ModelUser
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public Location DefaultLocation { get; set; }
    }
}
