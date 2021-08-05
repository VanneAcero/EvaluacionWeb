using Datos.Model;
using Datos.ModelosNuevos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionWeb.Controllers
{
    [Authorize]
    public class VehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public VehiculosController(EjercicioEvaluacionContext contex)
        {
            _context = contex;
        }
        [Authorize("Admin,User")]
        // GET: VehiculosController
        public ActionResult Index()
        {
            List<Vehiculo> listaVehiculo = _context.Vehiculos.ToList();

            return View(listaVehiculo);
        }
        [Authorize("Admin,User")]
        // GET: VehiculosController/Details/5
        public ActionResult Details(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }
        [Authorize("Admin")]
        // GET: VehiculosController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize("Admin")]
        // POST: VehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehiculo vehiculo)
        {
            try
            {
                vehiculo.Estado = 1;
                _context.Add(vehiculo);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(vehiculo);
            }
        }
        [Authorize("Admin")]
        // GET: VehiculosController/Edit/5
        public ActionResult Edit(int id )
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }
        [Authorize("Admin")]
        // POST: VehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
               _context.Update(vehiculo);
               _context.SaveChanges();
            return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize("Admin")]
        public ActionResult Desactivar(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 0;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize("Admin")]
        public ActionResult Activar(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 1;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
