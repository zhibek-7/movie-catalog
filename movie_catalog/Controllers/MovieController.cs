using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using movie_catalog.Models;
namespace movie_catalog.Controllers
{
    public class MovieController : Controller
    {

        private movieEntities _db = new movieEntities();
        // GET: Movie
        public ActionResult Index(int page = 1)
        {
          var movies = _db.movie.ToList();

         int pageSize = 3; // количество объектов на страницу
         IEnumerable<movie> moviesPerPages = movies.Skip((page - 1) * pageSize).Take(pageSize);
         PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = movies.Count };
         IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Movies = moviesPerPages };
         return View(ivm);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(users u)
        {

            using (movieEntities _db = new movieEntities())
            {
                var v = _db.users.Where(a => a.login.Equals(u.login) && a.password.Equals(u.password)).FirstOrDefault();
                if (v != null)
                {
                    Session["LogedUserId"] = v.id.ToString();
                    Session["LogedEmail"] = v.email.ToString();
                    Session["LogedUserName"] = v.userName.ToString();
                    Session["LogedPassword"] = v.password.ToString();
                    Session["LogedLogin"] = v.login.ToString();
                    FormsAuthentication.SetAuthCookie(u.userName, true);
                    return RedirectToAction("Index");
                }
                else {
                    return RedirectToAction("Login");
                }
            }
          //  return View(u);
        }


        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
           List<movie> movies = new List<movie>();
            //if (Session["LogedUserId"] != null)
            //{
              movie movie = new movie();
              movies = _db.movie.ToList();
              movie = (from m in _db.movie where m.id ==id select m).FirstOrDefault();
            if (movie != null)
            {
                return View(movie);
            }
            else {
                return RedirectToAction("Index");
            }
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            if (Session["LogedUserId"] != null)
            {
                movie amovie = new movie();
                return View(amovie);

            }
            else {
                return RedirectToAction("Index");
            }
           
          
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(movie amovie, HttpPostedFileBase file)
        {
            try
            {
                if (Session["LogedUserId"] != null)
                {
                    string userId = Session["LogedUserId"].ToString();
                    string login = Session["LogedLogin"].ToString();
                    amovie.userId = Convert.ToInt32(userId);
                    amovie.user = login;
                    var movie = (from m in _db.movie where m.userId == amovie.userId select m).FirstOrDefault();

                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));

                        file.SaveAs(path);
                        amovie.file = Path.GetFileName(file.FileName);

                        //movie.poster = new byte[image.ContentLength];
                        //var fghfh = movie.poster;
                        ///image.InputStream.Read(movie.poster, 0, image.ContentLength);
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";

                    if (movie == null)
                    {
                       _db.movie.Add(amovie);
                       _db.SaveChanges();
                    }


                }

                 
                //return View(amovie);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.FileStatus = "Error while file uploading." + e.Message;
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {

           

            if (Session["LogedUserId"] != null)
            {
                string userId = Session["LogedUserId"].ToString();
                string login = Session["LogedLogin"].ToString();
                movie amovie = _db.movie.Where(a => a.id == id).FirstOrDefault();
                if (amovie.userId == Convert.ToInt32(userId))
                {
                    movie amovie1 = new movie();
                    amovie1.dateBegin = amovie.dateBegin;
                    amovie1.description = amovie.description;
                    amovie1.nameMovie = amovie.nameMovie;
                    amovie1.poster = amovie.poster;
                    amovie1.producer = amovie.producer;
                    amovie1.userId = Convert.ToInt32(userId);
                    amovie1.user = login;
                    amovie1.file = amovie.file;
                    return View(amovie1);
                }
                else {
                    return RedirectToAction("Index");
                }
            
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,  movie amovie1,HttpPostedFileBase file)
        {
            try
            {
              
                if (Session["LogedUserId"] != null)
                {
                    string userId = Session["LogedUserId"].ToString();
                    string login = Session["LogedLogin"].ToString();
                    var amovie= _db.movie.Where(a => a.id.Equals(id)).FirstOrDefault();

                    if (amovie.userId == Convert.ToInt32(userId)) {


                        if (file != null)
                        {
                            string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));

                            file.SaveAs(path);
                            amovie.file = Path.GetFileName(file.FileName);

                            // amovie1.poster = new byte[image.ContentLength];
                            // image.InputStream.Read(amovie1.poster, 0, image.ContentLength);
                        }

                        if (amovie.id == id)
                        {
                            amovie.poster = amovie1.poster;
                            amovie.nameMovie = amovie1.nameMovie;
                            amovie.producer = amovie1.producer;
                            amovie.userId = Convert.ToInt32(userId);
                            amovie.dateBegin = amovie1.dateBegin;
                            amovie.description = amovie1.description;
                            amovie.user = login;
                            amovie.file = Path.GetFileName(file.FileName);
                        }
                        _db.SaveChanges();
                      
                    }
                    return RedirectToAction("Index");

                }
                else
                {
                    return RedirectToAction("Index");
                }


            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
