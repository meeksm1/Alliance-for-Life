using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.Models
{
    public class Year
    {
        public int Id { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Year")]
        public string Years { get; set; }
    }
}