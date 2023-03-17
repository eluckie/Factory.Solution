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
    public ActionResult Create(Machine thingy)
    {
      if(!ModelState.IsValid)
      {
        return View(thingy);
      }
      else
      {
        _db.Machines.Add(thingy);
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
  }
}