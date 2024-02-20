using Microsoft.AspNetCore.Mvc;
using MvcCoreEnfermosEF.Models;
using MvcCoreEnfermosEF.Repositories;

namespace MvcCoreEnfermosEF.Controllers
{
    public class DoctoresController : Controller
    {
        RepositoryDoctores repoDoc;

        public DoctoresController(RepositoryDoctores repo)
        {
            this.repoDoc = repo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctores = this.repoDoc.GetDoctores();
            ViewData["ESPECIALIDADES"] = this.repoDoc.GetDoctoresEspecialidad();
            return View(doctores);
        }

        [HttpPost]
        public IActionResult IncrementoSalario(string especialidad, int incremento)
        {
            this.repoDoc.IncrementoSalarios(especialidad, incremento);
            return RedirectToAction("Index");
        }
    }
}
