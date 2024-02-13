using Microsoft.AspNetCore.Mvc;
using MVC.Abstractions;
using MVC.Models;

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
    public async Task<ActionResult> Details(int id)
    {
        var model = await personService.GetPersonDetails(id);
        return View(model);
    }

    // POST: PersonController/Details/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(PersonViewModel person)
    {
        try
        {
            var response = await personService.CreatePerson(person);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        return View(person);
    }

    // PUT: PersonController/Details/5
    public async Task<ActionResult> Edit(int id)
    {
        var model = await personService.GetPersonDetails(id);
        return View(model);
    }

    // DELETE: PersonController/Details/5
    public async Task<ActionResult> Delete()
    {
        return View();
    }
}