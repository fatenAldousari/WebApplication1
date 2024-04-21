using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BankBranchController : Controller
    {
        static List<BankBranch> bankBranches = [
            new BankBranch{ Id ="1", LocationName="Kifan",LocationURL="https://www.bing.com/maps?osid=cceedb2d-6b45-4d10-96f8-1a26d85f7fae&cp=29.306768~47.923994&lvl=17&pi=0&v=2&sV=2&form=S00027",BranchManager="faten",EmployeeCount="3"},
            new BankBranch{ Id ="2", LocationName="zahra",LocationURL="https://www.bing.com/maps?osid=cceedb2d-6b45-4d10-96f8-1a26d85f7fae&cp=29.306768~47.923994&lvl=17&pi=0&v=2&sV=2&form=S00027",BranchManager="noura",EmployeeCount="5"}

            ];
        public IActionResult Index()
        {
            using (var context = new BankContext())
            {
                var bankBranches = context.BankBranches.ToList();
                return View(bankBranches);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(NewBranchForm form)
        {
            if (ModelState.IsValid)
            {
                var id = form.Id;
                var LocationName = form.LocationName;
                var LocationURL = form.LocationURL;
                var EmployeeCount = form.EmployeeCount;
                var BranchManager = form.BranchManager;

                using (var context = new BankContext())
                {
                    var bank = new BankBranch { Id = id, LocationName = LocationName, BranchManager = BranchManager, LocationURL = LocationURL, EmployeeCount = EmployeeCount };

                    context.BankBranches.Add(bank);
                    context.SaveChanges();


                    //bankBranches.Add(new BankBranch { Id = id, LocationName = LocationName, BranchManager=BranchManager, LocationURL = LocationURL, EmployeeCount = EmployeeCount });
                    return RedirectToAction("Index");
                }

            }
            return View(form);
        }
        public IActionResult Details(string id)
        {
            using (var context = new BankContext())
            {
                var bank = context.BankBranches.Find(id);
                if (bank != null)
                {
                    context.SaveChanges();
                }
                return View(bank);
            }
            // var bankBranch = bankBranches.FirstOrDefault(bankBranch => bankBranch.Id == id);

            // if (bankBranch == null)
            // {
            //    return RedirectToAction("Index");
            // }


            //  return View(bankBranch);
        }
        [HttpPost]
        public IActionResult Edit(string id, EditBranchForm newBranch) 
        {
            using(var context = new BankContext())
            {
                var bank = context.BankBranches.Find(id);
                if (bank != null)
                {
                    bank.LocationName = newBranch.LocationName;
                    bank.BranchManager = newBranch.BranchManager;
                    bank.LocationURL = newBranch.LocationURL;
                    bank.EmployeeCount = newBranch.EmployeeCount;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            using (var context = new BankContext())
            {
                var bank = context.BankBranches.Find(id);
                if (bank == null)
                { 
                  return NotFound();
                }
                var form = new EditBranchForm();
                form.Id = bank.Id;
                form.LocationName = bank.LocationName;
                form.BranchManager = bank.BranchManager;
                form.LocationURL = bank.LocationURL;    
                form.EmployeeCount = bank.EmployeeCount;
                return View(form);



            }

        }
    }
}

