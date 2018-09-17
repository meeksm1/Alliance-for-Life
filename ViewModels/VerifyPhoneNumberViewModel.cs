using System.ComponentModel.DataAnnotations;
using Alliance_for_Life.Models;

namespace Alliance_for_Life.ViewModels

{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}