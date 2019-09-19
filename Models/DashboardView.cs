using System;
using System.Collections.Generic;
namespace CSharpExam.Models
{
    public class DashboardView
    {
        public Idea Idea {get;set;}
        public List<Idea> AllIdeas {get;set;}
        public Like Like {get;set;}
    }
}