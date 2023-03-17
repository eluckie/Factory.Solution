using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers
        .Include(dude => dude.Repairs)
        .ThenInclude(join => join.Machine)
        .ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Engineer newGuy)
    {
      if(!ModelState.IsValid)
      {
        return View(newGuy);
      }
      else
      {
      _db.Engineers.Add(newGuy);
      _db.SaveChanges();
      return RedirectToAction("Index");
      }
    }
    public ActionResult Details(int id)
    {
      Engineer thisGuy = _db.Engineers
        .Include(dude => dude.Repairs)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(guy => guy.EngineerId == id);
      return View(thisGuy);
    }
    public ActionResult AddRepairs(int id)
    {
      Engineer thisGuy = _db.Engineers.FirstOrDefault(guy => guy.EngineerId == id);
      ViewBag.machines = _db.Machines
        .ToList();
      return View(thisGuy);
    }
    [HttpPost]
    public ActionResult AddRepairs(List<int> robots, int engineerId)
    {
      foreach (int robot in robots)
      {
        #nullable enable
        Repair? joinEntity = _db.Repairs.FirstOrDefault(entry => (entry.MachineId == robot && entry.EngineerId == engineerId));
        #nullable disable

        if (joinEntity == null && engineerId != 0)
        {
          Repair newRepair = new Repair();
          newRepair.MachineId = robot;
          newRepair.EngineerId = engineerId;
          _db.Repairs.Add(newRepair);
          _db.SaveChanges();
        }
      }
      return RedirectToAction("Index");
    }
  }
}