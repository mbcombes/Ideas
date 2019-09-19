using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpExam.Models
{
    public class User
    {
       [Key]
        public int UserId {get;set;}
        [Required(ErrorMessage="Name is requried.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage="Name can only contain letters and spaces.")]
        [MinLength(2, ErrorMessage="Name must be at least 2 characters long.")]
        [Display(Name="Name:")]
        public string Name {get;set;}
        [Required]
        [RegularExpression("^[0-9A-Za-z]+$", ErrorMessage="Name can only contain letters and numbers.")]
        [Display(Name="Alias:")]
        public string Alias {get;set;}
        [Required]
        [EmailAddress]
        [Display(Name="Email:")]
        public string Email {get;set;}
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password must contain at lest one upper case character, one lower case character, one number, and one special character.")]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters long.")]
        [Display(Name="Password:")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [NotMapped]
        [Required]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters long.")]
        [Compare("Password")]
        [Display(Name="Confirm Password:")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public List<Like> IdeasLiked {get;set;}
        public List<Idea> AllIdeas {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
       
        public User()
        {
            AllIdeas=new List<Idea>();
            IdeasLiked=new List<Like>();
            CreatedAt= DateTime.Now;
            UpdatedAt= DateTime.Now;
        }
    }
}