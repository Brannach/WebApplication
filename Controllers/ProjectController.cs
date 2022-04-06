using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProjectController : Controller
    {
        // GET: ProjectsController
        public ActionResult Index()
        {
            ProjectContext context = HttpContext.RequestServices.GetService(typeof(Models.ProjectContext)) as ProjectContext;

            return View(context.GetAllProjects());
        }

        // GET: ProjectsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            ProjectContext context = HttpContext.RequestServices.GetService(typeof(Models.ProjectContext)) as ProjectContext;
            try
            {
                StringValues id;
                StringValues name;
                collection.TryGetValue("id", out id);
                collection.TryGetValue("name", out name);
                context.InsertNewProject(id, name);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectsController/Edit/5
        public ActionResult Edit(int id)
        {
            ProjectContext context = HttpContext.RequestServices.GetService(typeof(Models.ProjectContext)) as ProjectContext;
            Project SelectedProject = context.GetProjectById(id);
            return View(SelectedProject);
        }

        // POST: ProjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                ProjectContext context = HttpContext.RequestServices.GetService(typeof(Models.ProjectContext)) as ProjectContext;
                StringValues name;
                collection.TryGetValue("name", out name);
                context.UpdateProject(new Project
                    {
                        id = id, 
                        name = name
                    }
                );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectsController/Delete/5
        public ActionResult Delete(int id)
        {
            ProjectContext context = HttpContext.RequestServices.GetService(typeof(Models.ProjectContext)) as ProjectContext;
            Project SelectedProject = context.GetProjectById(id);
            return View(SelectedProject);
        }

        // POST: ProjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
