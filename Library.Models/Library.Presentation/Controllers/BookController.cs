using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Presentation.Controllers;
using Library.Presentation.Models;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Dynamic;


namespace Library.Presentation.Controllers
{
    public class BookController : Controller
    {
        private LibraryContext db;
        IConfiguration con;

        
        public BookController(LibraryContext _db,IConfiguration _con)
        {
            db = _db;
            con = _con;
        }

        [ViewData]
        public UserInfo UserInfo { get; set; } = new UserInfo() {Name = "Test Aswan" };

        [HttpGet]
        public IActionResult Index(int pageIndex = 1, int pageSize = 2)
        {
            ViewBag.Title = "Books | Index";
            var books = db.Books.ToPagedList(pageIndex,pageSize);

            ViewBag.ImagesPath = con.GetSection("ImagesPath").Value.ToString();

            //ViewData["UserInfo"] = User;
            return View(books);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var book = db.Books.FirstOrDefault(i => i.ID == id);
            if(book != null)
            {
                ViewBag.ImagesPath = con.GetSection("ImagesPath").Value.ToString();
                //ViewData["UserInfo"] = User;
                return View(book);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Authors = db.Authors.Select(i => new SelectListItem(i.Name, i.ID.ToString()));
            return View();  
        }

        [HttpPost]
        public IActionResult Add(BookCreateModel model)
        {

            if (ModelState.IsValid == false) 
            {
                var errors = ModelState.SelectMany(i => i.Value.Errors.Select(x => x.ErrorMessage)).ToList();
                foreach (string err in errors)
                {
                    ModelState.AddModelError("", err);
                }

                ViewBag.Authors = db.Authors.Select(i => new SelectListItem(i.Name, i.ID.ToString()));
                ViewBag.Done = false;
                return View();
            }
            else
            {
                List<BookImage> images = new List<BookImage>();
                foreach (IFormFile file in model.Images)
                {
                    string NewName = Guid.NewGuid().ToString() + file.FileName;
                    images.Add(new BookImage
                    {
                        Path = NewName,
                    });
                    FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory()
                        , "Content", "Images", NewName),
                        FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    file.CopyTo(fs);
                    fs.Position = 0;
                }

                db.Books.Add(
                    new Book()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        AuthorID = model.AuthorID,
                        BookImages = images,
                    });
                db.SaveChanges();
                ViewBag.Authors = db.Authors.Select(i => new SelectListItem(i.Name, i.ID.ToString()));
                ViewBag.Done = true;
                return View();

            }
            
        }

        [HttpGet]
        public IActionResult ParitalBooks(int _pageIndex = 1, int _pageSize = 2)
        {
            var pagedBooks = db.Books.ToPagedList(_pageIndex, _pageSize);
            ViewBag.ImagesPath = con.GetSection("ImagesPath").Value.ToString();
            return PartialView("_PagedBooks", pagedBooks);
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var book = db.Books.FirstOrDefault(i => i.ID == id);
            //dynamic book = new ExpandoObject();
            //book.Title = title;
            //book.ID = id;
            return View(book);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = db.Books.FirstOrDefault(i => i.ID == id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Get");
        }
    }
}
