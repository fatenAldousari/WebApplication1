using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BankBranchController : Controller
    {
        private readonly BankContext _context;

        public BankBranchController(BankContext context)
        {
            _context = context;
        }
        static List<BankBranch> bankBranches = [
            new BankBranch{ Id =1, LocationName="Kifan",LocationURL="https://www.bing.com/maps?osid=cceedb2d-6b45-4d10-96f8-1a26d85f7fae&cp=29.306768~47.923994&lvl=17&pi=0&v=2&sV=2&form=S00027",BranchManager="faten",EmployeeCount="3"},
            new BankBranch{ Id =2, LocationName="zahra",LocationURL="https://www.bing.com/maps?osid=cceedb2d-6b45-4d10-96f8-1a26d85f7fae&cp=29.306768~47.923994&lvl=17&pi=0&v=2&sV=2&form=S00027",BranchManager="noura",EmployeeCount="5"}

            ];
        
        public IActionResult Index()
        {
            var viewModel = new BankDashboardViewModel();
            using (var context = _context)
            {
                viewModel.BranchList = context.BankBranches.ToList();
                viewModel.TotalEmployees = _context.Employees.Count();
                return View(viewModel);
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

                using (var context = _context)
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
        public IActionResult Details(int id)
        {
            using (var context = _context)
            {
                var bank = context.BankBranches.Include(r => r.Employees).FirstOrDefault(bankBranch => bankBranch.Id == id);
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
            using(var context = _context)
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
        public IActionResult Edit(int id)
        {
            using (var context =  _context)
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

        [HttpGet]
        public IActionResult AddEmployee(int Id)
        {
            
                return View();


        }
        [HttpPost]
        public IActionResult AddEmployee(int Id, AddEmployeeForm form)
        {
            if (ModelState.IsValid)
            {
                var database =  _context;
                var bank = database.BankBranches.Find(Id);
                var newemployee = new Employee();

               // newemployee.Id = Id;
                newemployee.Name = form.Name;
                newemployee.CivilId = form.CivilId;
                newemployee.Position = form.Position;


                bank.Employees.Add(newemployee);



                database.SaveChanges();
            }
            return View(form);
        }
    }
}

