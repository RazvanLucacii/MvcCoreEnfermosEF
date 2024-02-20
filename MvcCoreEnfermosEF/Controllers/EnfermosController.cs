using Microsoft.AspNetCore.Mvc;
using MvcCoreEnfermosEF.Models;
using MvcCoreEnfermosEF.Repositories;

namespace MvcCoreEnfermosEF.Controllers
{
    public class EnfermosController : Controller
    {
        RepositoryEnfermos repoEnf;

        public EnfermosController(RepositoryEnfermos repo)
        {
            this.repoEnf = repo;
        }
        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repoEnf.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult Details(int inscripcion)
        {
            Enfermo enfermo = this.repoEnf.FindEnfermo(inscripcion);
            return View(enfermo);
        }

        public IActionResult Delete(int inscripcion)
        {
            this.repoEnf.DeleteEnfermo(inscripcion);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Enfermo enfermo)
        {
            this.repoEnf.InsertEnfermo(enfermo.Apellido, enfermo.Direccion, enfermo.Fecha, enfermo.S, enfermo.NSS);
            return RedirectToAction("Index");
        }
    }
}
