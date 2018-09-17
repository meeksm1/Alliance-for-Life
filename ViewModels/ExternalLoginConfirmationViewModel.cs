using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}