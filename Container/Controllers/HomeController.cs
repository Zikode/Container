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
            return View();
        }

        public IQueryable<Containerobj> GetAll()
        {
            return _unitOfWorks.Container.GetAll();
        }
    }
}
