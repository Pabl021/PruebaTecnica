using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Propiedad()
        {
            return View();
        }

        //public IActionResult TipoPropiedad()
        //{
        //    List<TipoPropiedad> list = new List<TipoPropiedad>();
        //    using (var db =new Models.Db.quercuContext())
        //    {
        //        list = (from d in db.PropertyTypes select new TipoPropiedad 
        //        { 
        //            Id= d.Id,
        //            Descripcion= d.Description
        //        } ).ToList();
        //    }
        //    return View(list);
        //}

        //[HttpPost]
        //public IActionResult InsertType(TipoPropiedad tp)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            using (var db = new Models.Db.quercuContext())
        //            {
        //                var info = new TipoPropiedad();
        //                info.Id = tp.Id;
        //                info.Descripcion = tp.Descripcion;

        //                db.PropertyTypes.Add(info);
        //                db.SaveChanges();
        //            }
        //        }

        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}


        public IActionResult DeleteType(int id)
        {
            using (var db = new Models.Db.quercuContext())
            { 
                var info = db.PropertyTypes.Find(id);
                db.PropertyTypes.Remove(info);
                db.SaveChanges();
            }
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}