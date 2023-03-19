using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Machine> model = _db.Machines.ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Machine thing)
    {
      if(!ModelState.IsValid)
      {
        return View(thing);
      }
      else
      {
        _db.Machines.Add(thing);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    public ActionResult Details(int id)
    {
      Machine thisGuy = _db.Machines
        .Include(thing => thing.Repairs)
        .ThenInclude(join => join.Engineer)
        .FirstOrDefault(thing => thing.MachineId == id);
      return View(thisGuy);
    }
    public ActionResult Delete(int id)
    {
      Machine thisGuy = _db.Machines.FirstOrDefault(thing => thing.MachineId == id);
      return View(thisGuy);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisGuy = _db.Machines.FirstOrDefault(thing => thing.MachineId == id);
      _db.Machines.Remove(thisGuy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
      Machine thisGuy = _db.Machines.FirstOrDefault(thing => thing.MachineId == id);
      return View(thisGuy);
    }
    [HttpPost]
    public ActionResult Edit(Machine thing)
    {
      if(!ModelState.IsValid)
      {
        return View(thing);
      }
      else
      {
      _db.Machines.Update(thing);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = thing.MachineId });
      }
    }
    public ActionResult AddEngineer(int id)
    {
      Machine thisGuy = _db.Machines.FirstOrDefault(thing => thing.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisGuy);
    }
    [HttpPost]
    public ActionResult AddEngineer(Machine thing, int engineerId)
    {
      #nullable enable
      Repair? join = _db.Repairs.FirstOrDefault(join => (join.EngineerId == engineerId && join.MachineId == thing.MachineId));
      #nullable disable

      if (join == null && engineerId != 0)
      {
        _db.Repairs.Add(new Repair() { MachineId = thing.MachineId, EngineerId = engineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = thing.MachineId });
    }
    [HttpPost]
    public ActionResult DeleteRepair(int repairId, int machineId)
    {
      Repair join = _db.Repairs.FirstOrDefault(join => join.RepairId == repairId);
      _db.Repairs.Remove(join);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machineId });
    }
  }
}