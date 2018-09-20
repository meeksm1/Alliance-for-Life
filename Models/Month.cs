using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class Month
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Month")]
        public string Months { get; set; }
    }
}