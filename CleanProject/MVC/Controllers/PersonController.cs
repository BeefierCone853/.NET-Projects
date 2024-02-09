using Microsoft.AspNetCore.Mvc;
using MVC.Abstractions;

namespace MVC.Controllers;

public class PersonController(IPersonService personService) : Controller
{
    // GET: PersonController
    public async Task<ActionResult> Index()
    {
        var model = await personService.GetPersons();
        return View(model);
    }
    
    // GET: PersonController/Details/5
    public async Task<ActionResult> Details()
    {
        return View();
    }
    
    // POST: PersonController/Details/5
    public async Task<ActionResult> Create()
    {
        return View();
    }
    
    // PUT: PersonController/Details/5
    public async Task<ActionResult> Edit()
    {
        return View();
    }
    
    // DELETE: PersonController/Details/5
    public async Task<ActionResult> Delete()
    {
        return View();
    }
}