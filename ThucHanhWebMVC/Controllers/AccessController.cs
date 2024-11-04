using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using ThucHanhWebMVC.Models;
namespace ThucHanhWebMVC.Controllers
{
    
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]

        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DanhMucSanPham", "AdminHome", new { area = "Admin" });
            }


        }

        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName",u.Username.ToString());
                    //return RedirectToAction("Index", "Home");
                    //return RedirectToAction("Index", "", new { area = "Admin" });
                    return RedirectToAction("DanhMucSanPham", "AdminHome", new { area = "Admin" });
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

        public IActionResult Index()
        {
            return View();
        }


        // Thêm mới phương thức SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(TUser newUser)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem Username có tồn tại chưa
                var existingUser = db.TUsers.FirstOrDefault(x => x.Username == newUser.Username);
                if (existingUser == null)
                {
                    // Thêm user mới vào database
                    db.TUsers.Add(newUser);
                    db.SaveChanges();

                    // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                    TempData["SuccessMessage"] = "Tài khoản đã được tạo thành công! Vui lòng đăng nhập.";
                    return RedirectToAction("Login","Access");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.");
                }
            }
            return View(newUser);
        }

    }
}
