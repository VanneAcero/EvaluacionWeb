using Datos.Model;
using Datos.ModelosNuevos;
using Datos.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionWeb.Controllers
{
    [Authorize]
    public class TipoVehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public TipoVehiculosController(EjercicioEvaluacionContext contex)
        {
            _context = contex;
        }
        public void Combox()
        {
            ViewData["CodigoVehiculo"] = new SelectList(_context.Vehiculos.Select(x => new ViewModelTipoVehiculo
            {
                Codigo = x.Codigo,
                Nombres = $"{x.Nombre}",
                Estado = x.Estado

            }).Where(s => s.Estado == 1).ToList(), "Codigo", "Nombres");
        }
        [Authorize ("Admin,User")]
        // GET: VehiculosController
        public ActionResult Index()
        {
            //List<TipoVehiculo> listaTipoVehiculo = _context.TipoVehiculos.ToList();
            List<ViewModelTipoVehiculo> listaTipoVehiculo = _context.TipoVehiculos.Select(x => new ViewModelTipoVehiculo
            {
                CodigoVehiculo = x.Codigo,
                DescripcionVehiculo = x.Descripcion,
                Nombres = $"{x.CodigoVehiculoNavigation.Nombre}",
                Estado = x.Estado

            }).ToList();
            return View(listaTipoVehiculo);
        }
        [Authorize("Admin,User")]
        // GET: TipoVehiculosController/Details/5
        public ActionResult Details(int id)
        {
            TipoVehiculo tipvehiculo = _context.TipoVehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(tipvehiculo);
        }
        [Authorize("Admin")]
        // GET: TipoVehiculosController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }
        [Authorize("Admin")]
        // POST: TipoVehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVehiculo tipvehiculo)
        {
            try
            {
                tipvehiculo.Estado = 1;
                _context.Add(tipvehiculo);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipvehiculo);
            }
        }
        [Authorize("Admin")]
        // GET: TipoVehiculosController/Edit/5
        public ActionResult Edit(int id)
        {
            Combox();
            TipoVehiculo tipvehiculo = _context.TipoVehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(tipvehiculo);
        }
        [Authorize("Admin")]
        // POST: TipoVehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoVehiculo tipvehiculo)
        {
            if (id != tipvehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(tipvehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View();
            }
        }
        [Authorize("Admin")]
        public ActionResult Desactivar(int id)
        {
            TipoVehiculo tipvehiculo = _context.TipoVehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            tipvehiculo.Estado = 0;
            _context.Update(tipvehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize("Admin")]
        public ActionResult Activar(int id)
        {
            TipoVehiculo tipvehiculo = _context.TipoVehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            tipvehiculo.Estado = 1;
            _context.Update(tipvehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
