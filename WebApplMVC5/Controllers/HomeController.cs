using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplMVC5.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace WebApplMVC5.Controllers
{
    public class HomeController : Controller
    {

        Uri baseurl = new Uri("https://localhost:44350/api");
        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseurl;
        }

        //[OutputCache(CacheProfile = "Cache20sec")]
        public ActionResult Index()
        {
            System.Threading.Thread.Sleep(3000);
            List<User> users = new List<User>();
            ViewBag.Message = "<h1>Return to Microsoft Teams</h1>. When the release pipeline is ready to deploy, you will receive an approval notification in Teams. Click Approve to approve the release.";
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(data);

            }
            return View(users);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 20)]

        public string Caching()
        {
            return "cached time = " + DateTime.Now.ToString();
        }
        public ActionResult GetUser(int id)
        {

            User user = new User();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);

            }
            return View(user);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/user", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();
        }
        [OutputCache(CacheProfile = "Cache20secbyparam")]
        public ActionResult Edit(int id)
        {
            User user = new User();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);

            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/user/" + user.id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/user/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        //public ActionResult DeleteUser()
        //{
        //    return RedirectToAction("CheckList");
        //}
        
        public ActionResult DeleteUser(IEnumerable<int> userIdtoDelete)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/user/" + userIdtoDelete).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CheckList");
            }
            return RedirectToAction("CheckList");
        }

        //[ActionName("Test")]
        //[NonAction]
        //[HttpGet]
        public ActionResult Image()
        {
            return View();
        }
        public ActionResult StringHelperCheck()
        {
            string upper = "harsha";
            string lower = "SAI";
            string nullString = null;
            List<string> stringList = new List<string>();
            stringList.Add(upper.ChangeFirstLetterUpper());
            stringList.Add(lower.ChangeFirstLetterLower());
            stringList.Add(nullString.IsNullChnageToEmpty());

            ViewBag.strings = stringList;


            return View();
        }
        public ActionResult ImageValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageValidation(HttpPostedFileBase file)
        {
            //give max file size in KB example => 1024KB = 1MB
            if (file.IsImageValid(2048) == true)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Uploadfiles"), fileName);
                file.SaveAs(path);
                ViewBag.message = "Image Uploaded suceessfully";

                return View();
            }
            else
            {
                ViewBag.message = "Image Upload Failed!!!";
                return View();
            }


        }
        public ActionResult FileValidation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FileValidation(HttpPostedFileBase file)
        {
            if (file.IsFileValid(5120) == true)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Uploadfiles"), fileName);
                file.SaveAs(path);
                ViewBag.message = "File Uploaded suceessfully";

                return View();
            }
            else
            {
                ViewBag.message = "File Upload Failed!!!";
                return View();
            }

        }
        public ActionResult CheckList()
        {
            List<User> users = new List<User>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(data);

            }
            return View(users);
        }
        public ActionResult SortingFliteringUsers()
        {
            List<User> users = new List<User>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(data);

            }
            return View(users);
        }
        [HttpPost]
        public JsonResult Lazyloading(int pageNumber,int pageSize)
        {
            int startRow = ((pageNumber - 1) * pageSize) + 1;
            int endRow = pageNumber * pageSize;
            List<User> data = new List<User>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user" ).Result;
            if (response.IsSuccessStatusCode)
            {
                string userdata = response.Content.ReadAsStringAsync().Result;
                List<User> allData = JsonConvert.DeserializeObject<List<User>>(userdata);
                 data= allData.OrderBy(x => x.id).Where(x => x.id >= startRow && x.id <= endRow).ToList<User>();

            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Lazyloading()
        {
            return View();
        }
    }
}