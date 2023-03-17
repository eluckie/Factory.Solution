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
      ViewBag.Machines = _db.Machines.ToList();
      return View(thisGuy);
    }
    [HttpPost]
    public ActionResult AddRepairs(List<int> robots, int engineerId)
    {
      foreach (int robot in robots)
      {
        #nullable enable
        Repair? joinEntity = _db.Repairs.FirstOrDefault(entry => (entry.EngineerId == robot && entry.EngineerId == engineerId));
        #nullable disable

        if (joinEntity == null && engineerId != 0)
        {
          _db.Repairs.Add(new Repair() { MachineId = robot, EngineerId = engineerId});
          _db.SaveChanges();
        }
      }
      return RedirectToAction("Details", new { id = engineerId });
    }
    public ActionResult Delete(int id)
    {
      Engineer thisGuy = _db.Engineers.FirstOrDefault(thing => thing.EngineerId == id);
      return View(thisGuy);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer thisGuy = _db.Engineers.FirstOrDefault(thing => thing.EngineerId == id);
      _db.Engineers.Remove(thisGuy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteRepair(int repairId, int engineerId)
    {
      Repair join = _db.Repairs.FirstOrDefault(join => join.RepairId == repairId);
      _db.Repairs.Remove(join);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineerId });
    }
  }
}