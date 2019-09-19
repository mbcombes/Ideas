using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpExam.Models
{
    public class Idea
    {
        [Key]
        public int IdeaId {get;set;}
        [MinLength(5)]
        [Required]
        public string Content {get;set;}
        [Required]
        public int UserId {get;set;}
        public User Creator {get;set;}
        public List<Like> Likes {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        public Idea()
        {
            Likes=new List<Like>();
            CreatedAt= DateTime.Now;
            UpdatedAt= DateTime.Now;
        }
        public bool UserLiked(int userid)
        {
            foreach(var like in Likes)
            {
                if(like.UserId==userid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}