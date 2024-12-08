using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Company name")]
        public string? Name { get; set; }
        [DisplayName("City")]
        public string? City { get; set; }
        [DisplayName("Address")]
        public string? Address { get; set; }
        [DisplayName("Number")]
        public string? Number { get; set; }
        
    }
}
