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
    }
}
