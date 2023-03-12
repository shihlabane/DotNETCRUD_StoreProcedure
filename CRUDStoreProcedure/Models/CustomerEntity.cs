using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDStoreProcedure.Models
{
    public class CustomerEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        [Display(Name= "Contact Number")]
        public string Mobile { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
