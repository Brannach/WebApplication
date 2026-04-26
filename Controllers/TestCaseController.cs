using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TestCaseController : Controller
    {
        public ActionResult Index()
        {
            var context = HttpContext.RequestServices.GetService(typeof(TestCaseContext)) as TestCaseContext;
            return View(context.GetAllTestCases());
        }
    }
}
