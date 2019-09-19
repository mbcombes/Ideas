using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpExam.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email:")]
        public string EmailLogin {get;set;}
        [Required]
        [MinLength(8)]
        [Display(Name="Password:")]
        [DataType(DataType.Password)]
        public string PasswordLogin {get;set;}
    }
}