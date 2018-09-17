using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}