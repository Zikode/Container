using Container.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Repo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Container.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWorkRepository _unitOfWorks;

        public HomeController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWorks = unitOfWork;
        }

        public IActionResult Index()
        {
            var results = _unitOfWorks._container.GetAll();
            return View(results);
        }

        public IActionResult InsertNewContainer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertNewContainer(Containerobj container)
        {
            if (container == null)
            {
                return NotFound();
            }
            else
            {
                var result = _unitOfWorks._container.Add(container);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateContainer(int Id)
        {
            var container = _unitOfWorks._container.Get(Id);
            if(container == null)
            {
                return NotFound();
            }
            else
            {
                return View(container);
            }
        }

        public IActionResult UpdateContainerById(Containerobj container)
        {
            if (container == null)
            {
                return NotFound();
            }
            else
            {
                var result = _unitOfWorks._container.Update(container);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
