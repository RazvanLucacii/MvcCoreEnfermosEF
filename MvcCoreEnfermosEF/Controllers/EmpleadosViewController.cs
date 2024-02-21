using Microsoft.AspNetCore.Mvc;
using MvcCoreEnfermosEF.Models;
using MvcCoreEnfermosEF.Repositories;

namespace MvcCoreEnfermosEF.Controllers
{
    public class EmpleadosViewController : Controller
    {
        RepositoryViewEmpleados repoEmp;

        public EmpleadosViewController(RepositoryViewEmpleados repoEmp)
        {
            this.repoEmp = repoEmp;
        }

        public async Task<IActionResult> Index()
        {
            List<ViewEmpleado> model = await this.repoEmp.GetEmpleadosAsync();
            return View(model);
        }
    }
}
