using FSTodoList.Models;

namespace FSTodoList.ViewModels;

public class ListTodoViewModel
{
    public ICollection<Todo> Todos { get; set;} = new List<Todo>();
}