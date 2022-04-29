using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;
using PruebaTecnica.Models.Db;

namespace PruebaTecnica.Controllers
{
    public class TipoPropiedadController : Controller
    {
        // GET: TipoPropiedadController
        public ActionResult Index()
        {
            List<TipoPropiedad> list = new List<TipoPropiedad>();

            using (var db = new Models.Db.quercuContext())
            {
                list = (from d in db.PropertyTypes
                        select new TipoPropiedad
                        {
                            Id = d.Id,
                            Descripcion = d.Description
                        }).ToList();
            }
            return View(list);
        }

        // GET: TipoPropiedadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoPropiedadController/Create
        [HttpPost]
        public ActionResult Create(TipoPropiedad t)
        {
           
            var newType = new PropertyType();
            newType.Description = t.Descripcion;


            using (var db = new Models.Db.quercuContext())
            {
                db.PropertyTypes.Add(newType);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
            //return View();
        }



        // GET: TipoPropiedadController/Edit/5
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
                    var info = await db.PropertyTypes.FindAsync(id);
                    if (info == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var result = new TipoPropiedad()
                        {
                            Id = info.Id,
                            Descripcion = info.Description
                        };
                        return View("Editar", result);
                    }
                }

            }

        }

        // POST: TipoPropiedadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Descripcion")] TipoPropiedad tp)
        {
            if (id != tp.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new Models.Db.quercuContext())
                    {
                        var entry = db.PropertyTypes.First(e => e.Id == id);

                        var tpToUpdate = new PropertyType
                        {
                            Id = tp.Id,
                            Description = tp.Descripcion
                        };

                        if (entry == null)
                        {
                            return NotFound();
                        }
                        db.Entry(entry).CurrentValues.SetValues(tpToUpdate);
                        await db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tp);
        }

        // GET: TipoPropiedadController/Delete/5
        public ActionResult Delete(int id)
        {
            using (var db = new Models.Db.quercuContext())
            {
                var info = db.PropertyTypes.Find(id);
                db.PropertyTypes.Remove(info);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }





    }
}
