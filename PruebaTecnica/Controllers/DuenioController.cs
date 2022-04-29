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
            using (var db = new Models.Db.quercuContext())
            {
                var info = db.Owners.Find(id);
                db.Owners.Remove(info);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                using (var db = new Models.Db.quercuContext())
                {
                    var info = await db.Owners.FindAsync(id);
                    if (info == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var result = new Duenio()
                        {
                            Id = info.Id,
                            Name = info.Name,
                            Telephone = info.Telephone,
                            Email = info.Email,
                            IdentificationNumber = info.IdentificationNumber,
                            Address = info.Address
                            
                        };
                        return View("Editar", result);
                    }
                }

            }

        }

        // POST: TipoPropiedadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id", "Name", "Telephone","Email", "IdentificationNumber", "Address")] Duenio du)
        {
            if (id != du.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new Models.Db.quercuContext())
                    {
                        var data = db.Owners.First(e => e.Id == id);

                        var duToUpdate = new Owner
                        {
                            Id = du.Id,
                            Name=du.Name,
                            Telephone=du.Telephone,
                            Email=du.Email,
                            IdentificationNumber=du.IdentificationNumber,
                            Address=du.Address                           
                        };

                        if (data == null)
                        {
                            return NotFound();
                        }
                        db.Entry(data).CurrentValues.SetValues(duToUpdate);
                        await db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(du);
        }

    }
}
