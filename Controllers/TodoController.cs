using FSTodoList.Contexts;
using FSTodoList.Models;
using FSTodoList.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FSTodoList.Controllers;

public class TodoController : Controller
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var todos = _context.Todos.OrderBy(x => x.Date).ToList();
        var viewModel = new ListTodoViewModel {Todos = todos};
        ViewData["Title"] = "Lista de Tarefas";
        
        return View(viewModel);
    }

    public IActionResult Delete(int id)
    {
        var todo = _context.Todos.Find(id);

        if (todo is null)
        {
            return NotFound();
        }

        _context.Remove(todo);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        ViewData.Add("Title", "Cadastrar Tarefa");
        return View("Form");
    }

    [HttpPost]
    public IActionResult Create(FormTodoViewModel data)
    {
        var todo = new Todo(data.Title, data.Date);
        _context.Add(todo);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        ViewData.Add("Title", "Editar Tarefa");        

        var todo = _context.Todos.Find(id);
        if(todo is null)
        {
            return NotFound();
        }
        var viewModel = new FormTodoViewModel {Title = todo.Title, Date = todo.Date};
        return View("Form", viewModel);
    }

    [HttpPost]
    public IActionResult Edit(int id, FormTodoViewModel data)
    {
        var todo = _context.Todos.Find(id);
        if(todo is null)
        {
            return NotFound();
        }
        todo.Title = data.Title;
        todo.Date = data.Date;
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult ToComplete(int id)
    {
        var todo = _context.Todos.Find(id);
        if(todo is null)
        {
            return NotFound();
        }
        todo.IsCompleted = true;
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}