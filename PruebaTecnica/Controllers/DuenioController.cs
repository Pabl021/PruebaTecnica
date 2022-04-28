using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;
using PruebaTecnica.Models.Db;
namespace PruebaTecnica.Controllers
{
    public class DuenioController : Controller
    {
        // GET: Duenio
        public ActionResult Index()
        {
            List<Duenio> list = new List<Duenio>();
            using (var db = new Models.Db.quercuContext())
            {
                list = (from d in db.Owners
                        select new Duenio
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Telephone = d.Telephone,
                            Email = d.Email,
                            IdentificationNumber = d.IdentificationNumber,
                            Address = d.Address
                            
                        }).ToList();
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Create(Duenio d)
        {
            var newOwner = new Owner();
            newOwner.Name = d.Name;
            newOwner.Telephone = d.Telephone;
            newOwner.Email = d.Email;
            newOwner.IdentificationNumber = d.IdentificationNumber;
            newOwner.Address = d.Address;

            using (var db = new Models.Db.quercuContext())
            {
                db.Owners.Add(newOwner);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Duenio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

      
    }
}
