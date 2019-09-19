
using Microsoft.EntityFrameworkCore;

namespace CSharpExam.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }   
        public DbSet<User> Users {get; set;}
        public DbSet<Idea> Ideas {get;set;}
        public DbSet<Like> Likes {get;set;}
    }
}