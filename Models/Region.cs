using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class Region
    {
        [Required]
        [StringLength(255)]
        public string Regions { get; set; }

        public int Id { get; set; }
    }
}