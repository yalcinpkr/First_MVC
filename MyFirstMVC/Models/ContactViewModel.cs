using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyFirstMVC.Models
{
    public class ContactViewModel
    {
        [Display(Name="Ad")]
        [MaxLength(20)]
        [Required(ErrorMessage ="Ad Alanı Gereklidir")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Soyad Alanı Gereklidir")]
        public string LastName { get; set; }

        [Display(Name = "Telefon")]
        [MaxLength(20)]
        [Required]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [MaxLength(100)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Mesaj")]
        [MaxLength(4000)]
        [Required]
        public string Message { get; set; }

        [Required]
        public bool PrivacyPolicyAccepted { get; set; }
    }
}