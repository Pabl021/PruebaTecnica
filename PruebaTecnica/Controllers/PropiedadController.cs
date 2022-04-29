using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;
using PruebaTecnica.Models.Db;


namespace PruebaTecnica.Controllers
{
    public class PropiedadController : Controller
    {

        // GET: PropiedadController
        public ActionResult Index()
        {

            List<Propiedad> list = new List<Propiedad>();
            ResumenPropiedad r = new();

            using (var db = new Models.Db.quercuContext())
            {
                list = (from d in db.Properties
                        select new Propiedad
                        {
                            Id = d.Id,
                            PropertyTypeId = d.PropertyTypeId,
                            OwnerId = d.OwnerId,
                            Number = d.Number,
                            Address = d.Address,
                            Area = d.Area,
                            ConstructionArea = d.ConstructionArea

                        }).ToList();

                r.Propiedades = list;
                r.TipoPropiedades = CargarTipos();
                r.Duenio = CargarDuenios();
            }
            return View(r);

        }

        public List<TipoPropiedad> CargarTipos()
        {
            List<TipoPropiedad> listTipos = new List<TipoPropiedad>();
            using (var db = new Models.Db.quercuContext())
            {
                listTipos = (from d in db.PropertyTypes
                             select new TipoPropiedad
                             {
                                 Id = d.Id,
                                 Descripcion = d.Description
                             }).ToList();
            }
            return listTipos;
        }

        public List<Duenio> CargarDuenios()
        {
            List<Duenio> listDuenios = new List<Duenio>();
            using (var db = new Models.Db.quercuContext())
            {
                listDuenios = (from d in db.Owners
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
            return listDuenios;
        }


        public ActionResult Delete(int id)
        {
            using (var db = new Models.Db.quercuContext())
            {
                var info = db.Properties.Find(id);
                db.Properties.Remove(info);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


        public ActionResult Create(Propiedad p)
        {
            var newPro = new Property();
            
            newPro.PropertyTypeId = p.PropertyTypeId;
            newPro.OwnerId = p.OwnerId;
            newPro.Number = p.Number;
            newPro.Address = p.Address;
            newPro.Area = p.Area;
            newPro.ConstructionArea = p.ConstructionArea;

            using (var db = new Models.Db.quercuContext())
            {
                db.Properties.Add(newPro);
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
                    var info = await db.Properties.FindAsync(id);
                    if (info == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var result1 = new ResumenPropiedad()
                        {
                            Id = info.Id,
                            PropertyTypeId = info.PropertyTypeId,
                            OwnerId = info.OwnerId,
                            Number = info.Number,
                            Address = info.Address,
                            Area = info.Area,
                            ConstructionArea = info.ConstructionArea

                        };
                        return View("Editar", result1);
                    }
                }

            }

        }

        // POST: TipoPropiedadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id", "PropertyTypeId", "OwnerId", "Number", "Address", "Area", "ConstructionArea")] Propiedad pro)
        {
            if (id != pro.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new Models.Db.quercuContext())
                    {
                        var data = db.Properties.First(e => e.Id == id);

                        var proToUpdate = new Property
                        {
                            Id = pro.Id,
                            PropertyTypeId = pro.PropertyTypeId,
                            OwnerId = pro.OwnerId,
                            Number = pro.Number,
                            Address = pro.Address,
                            Area = pro.Area,
                            ConstructionArea = pro.ConstructionArea
                        };

                        if (data == null)
                        {
                            return NotFound();
                        }
                        db.Entry(data).CurrentValues.SetValues(proToUpdate);
                        await db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pro);
        }



    }
}

