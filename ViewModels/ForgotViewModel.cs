using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}