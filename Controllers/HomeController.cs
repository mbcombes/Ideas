using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSharpExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CSharpExam.Controllers
{
    public class HomeController : Controller
    {
        private Context DbContext;

        public HomeController(Context context)
        {
            DbContext=context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/register")]
        [HttpPost]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                System.Console.WriteLine("modelstate is valid register");
                if(DbContext.Users.Any(u => u.Email == user.Email))
                {
                    System.Console.WriteLine("email is already in use");
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                DbContext.Add(user);
                DbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId",user.UserId);
                HttpContext.Session.SetString("Name",user.Name);
                System.Console.WriteLine("User Created");
                return RedirectToAction("Dashboard");
            }
            else{
                System.Console.WriteLine("Failed Modelstate");
                return View("Index");
            }
        }

        [Route("/login")]
        [HttpGet]
        public IActionResult ShowLogin()
        {
            return View("Login");
        }

        [Route("/login")]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            if(ModelState.IsValid)
            {
                System.Console.WriteLine("ModelState Valid");
                var userInDb = DbContext.Users.FirstOrDefault(u => u.Email == login.EmailLogin);
                if(userInDb == null)
                {
                    System.Console.WriteLine("email not in db");
                    ModelState.AddModelError("email", "Invalid Email/Password");
                    return View("Login");
                }
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(login, userInDb.Password, login.PasswordLogin);
                if(result ==0)
                {
                    System.Console.WriteLine("pw not in db");
                    ModelState.AddModelError("email", "Invalid Email/Password");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserId",userInDb.UserId);
                HttpContext.Session.SetString("Name",userInDb.Name);
                return RedirectToAction("Dashboard");
            }
            else{
                return View("Login");
            }
        }

        [Route("/destroy")]
        [HttpGet]
        public IActionResult Destroy()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [Route("/Dashboard")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetString("UserId") != null)
            {
                ViewBag.UserId=HttpContext.Session.GetInt32("UserId");
                ViewBag.UserName=HttpContext.Session.GetString("Name");
                DashboardView view = new DashboardView()
                {
                    AllIdeas=DbContext.Ideas
                        .Include(user => user.Creator)
                        .Include(ideas => ideas.Likes)
                        .ThenInclude(likes => likes.User)
                        .OrderByDescending(l => l.Likes.Count)
                        .ToList(),
                };
                return View(view);
            }
            else{
                return RedirectToAction("Index");
            }
        }
        [Route("/Idea/Create")]
        [HttpPost]
        public IActionResult CreateIdea(DashboardView data)
        {
            Idea form=data.Idea;
            if(ModelState.IsValid)
            {
                DbContext.Ideas.Add(form);
                DbContext.SaveChanges();
                System.Console.WriteLine("Idea Added");
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.UserId=HttpContext.Session.GetInt32("UserId");
                ViewBag.UserName=HttpContext.Session.GetString("Name");
                DashboardView view = new DashboardView()
                {
                    AllIdeas=DbContext.Ideas
                        .Include(user => user.Creator)
                        .Include(ideas => ideas.Likes)
                        .ThenInclude(likes => likes.User)
                        .ToList(),
                };
                System.Console.WriteLine("Idea Failed");
                return View("Dashboard", view);
            }
        }
        [HttpGet("/idea/destroy/{id}")]
        public IActionResult DestroyIdea(int id)
        {
            Idea thisidea= DbContext.Ideas.FirstOrDefault(a =>a.IdeaId==id);
            DbContext.Remove(thisidea);
            DbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpPost("/like/create")]
        public IActionResult CreateLike(DashboardView data)
        {
            Like form=data.Like;
            Idea ThisIdea=DbContext.Ideas
                .Include(idea => idea.Likes)
                .ThenInclude(likes => likes.User)
                .FirstOrDefault(a => a.IdeaId==form.IdeaId);
            if(ThisIdea.UserLiked(form.UserId)==false)
            {
                DbContext.Likes.Add(form);
                DbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                System.Console.WriteLine("Like not added.");
                return RedirectToAction("Dashboard");
            }
        }
        [HttpGet("/Idea/{ideaid}")]
        public IActionResult ShowIdea(int ideaid)
        {
            Idea ThisIdea = DbContext.Ideas
                .Include(idea => idea.Creator)
                .Include(idea => idea.Likes)
                .ThenInclude(likes => likes.User)
                .FirstOrDefault(a => a.IdeaId==ideaid);
            return View("IdeaShow", ThisIdea);
        } 
        [HttpGet("/Users/{userid}")]
        public IActionResult ShowUser(int userid)
        {
            User ThisUser = DbContext.Users
                .Include(use => use.IdeasLiked)
                .ThenInclude(liked => liked.Idea)
                .FirstOrDefault(a => a.UserId==userid);
            return View(ThisUser);
        }
    }
}
