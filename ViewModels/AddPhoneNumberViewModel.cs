using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}