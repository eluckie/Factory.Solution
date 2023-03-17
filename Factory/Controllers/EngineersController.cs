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
  }
}