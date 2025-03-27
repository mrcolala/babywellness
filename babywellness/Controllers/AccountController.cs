using babywellness.Data;
using babywellness.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _db;

    public IActionResult Login() => View();
    public IActionResult Register() => View();

    public AccountController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Hash entered password
        using var sha256 = SHA256.Create();
        var hash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password)));

        // Look up user
        var user = _db.Users.FirstOrDefault(u => u.Username == model.Username && u.PasswordHash == hash);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        // Store basic session (or use cookie)
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("DisplayName", $"{user.Name} {user.Lastname}");

        return RedirectToAction("Index", "Home");
    }


    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Check if username is already taken
        if (_db.Users.Any(u => u.Username == model.Username))
        {
            ModelState.AddModelError("Username", "Username is already taken");
            return View(model);
        }

        // Hash the password
        using var sha256 = SHA256.Create();
        var hash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password)));

        // Save user
        var user = new User
        {
            Name = model.Name,
            Lastname = model.Lastname,
            Birthday = model.Birthday,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Username = model.Username,
            PasswordHash = hash,
            CreatedAt = DateTime.Now
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        return RedirectToAction("Login");
    }



}
