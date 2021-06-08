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
                var check = _unitOfWorks._container.Add(container);
                if (check <= 0)
                {
                    TempData["Message"] = "Container not saved succesfully";
                    return View();
                }
                else
                {
                    TempData["Message"] = "Container saved succesfully";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult Edit(int Id)
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

        [HttpPost]
        public IActionResult Edit(Containerobj container)
        {
            if (container == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWorks._container.Update(container);
            }
            TempData["Message"] = "Container updated succesfully";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int Id)
        {
            if(Id <= 0)
            {
                return NotFound();
            }
            else
            {
                _unitOfWorks._container.Delete(Id);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetContainerByContainerNumber(int containerNumber)
        {
            if(containerNumber == 0)
            {
                return NotFound();
            }
            else
            {
                _unitOfWorks._container.GetContainerByContainerNumber(containerNumber);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
