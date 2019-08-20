using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Required]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description cannot exceed 150 characters")]
        public string Desc { get; set; }

        [Required]
        [MaxLength (3, ErrorMessage = "Please enter a number between 0 and 100")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid whole positive number")]
        public string Quantity { get; set; }
    }
}
