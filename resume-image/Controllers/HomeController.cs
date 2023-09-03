using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using resume_image.IRepository;
using resume_image.Models;
using System.Diagnostics;
using NewtonsoftJson = Newtonsoft.Json; // Alias for Newtonsoft.Json.JsonConvert

namespace resume_image.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository _userRepo = null;

        public HomeController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SaveFile(FileUpload fileObj)
        {
            User User_ = NewtonsoftJson.JsonConvert.DeserializeObject<User>(fileObj.User);

            if (fileObj.file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileObj.file.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    User_.Photo = fileBytes;
                    User_ = _userRepo.Save(User_);
                    if (User_.Id.Trim() != "")
                    {
                        return "saved";
                    }
                }
            }

            return "Failed";
        }

        [HttpGet]

        public JsonResult GetSavedUser() 
        {
            var user_ = _userRepo.GetSaveUser();
            user_.Photo = this.GetImage(Convert.ToBase64String(user_.Photo));
            return Json(user_);
        }

        public byte[] GetImage(string sBase64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(sBase64String)) 
            {
                bytes = Convert.FromBase64String(sBase64String);
            }
            return bytes;
        }
    }
}
