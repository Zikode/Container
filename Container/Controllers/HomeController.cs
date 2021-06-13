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

        public IActionResult Index(string sortOrder, string keyWord, string filiter, int? pagenumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Containernumber_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (keyWord != null)
            {
                pagenumber = 1;
            }
            else
            {
                keyWord = filiter;
            }

            ViewBag.CurrentFilter = keyWord;

            var results = _unitOfWorks._container.GetAll();

            if (!String.IsNullOrEmpty(keyWord))
            {
                results = results.Where(s => s.ContainerNumber.ToString().Contains(keyWord)
                                       || s.Code.Contains(keyWord));
            }
            switch (sortOrder)
            {
                case "Containernumber_desc":
                    results = results.OrderByDescending(s => s.ContainerNumber);
                    break;
                case "Code":
                    results = results.OrderByDescending(s => s.Code);
                    break;
                case "color":
                    results = results.OrderByDescending(s => s.DateAdded);
                    break;
                case "date_desc":
                    results = results.OrderByDescending(s => s.DateAdded);
                    break;
                default:
                    results = results.OrderByDescending(s => s.ContainerID);
                    break;
            }
            TempData["isAdd"] = null;
            return View(results);
        }

    public IActionResult InsertNewContainer()
        {
            TempData["isAdd"] = "isAdd";
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
                    TempData["Color"] = "btn btn-danger";
                }
                else
                {
                    TempData["Message"] = "Container saved succesfully";
                    TempData["Color"] = "btn btn-success";
                }
            }
            return RedirectToAction("Index", "Home");
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
             var check = _unitOfWorks._container.Update(container);
                if (check <= 0)
                {
                    TempData["Message"] = "Container not updated succesfully";
                    TempData["Color"] = "btn btn-danger";
                }
                else
                {
                    TempData["Message"] = "Container updated succesfully";
                    TempData["Color"] = "btn btn-success";
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int Id)
        {
            if(Id <= 0)
            {
                TempData["Message"] = "Container not updated succesfully";
                TempData["Color"] = "btn btn-danger";
                return NotFound();
            }
            else
            {
                TempData["Message"] = $"Container {Id} deleted succesfully";
                TempData["Color"] = "btn btn-success";
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
