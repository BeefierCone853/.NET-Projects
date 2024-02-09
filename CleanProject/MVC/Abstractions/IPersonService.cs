using MVC.Models;
using MVC.Services.Base;

namespace MVC.Abstractions;

public interface IPersonService
{
    Task<List<PersonViewModel>> GetPersons();
    Task<PersonViewModel> GetPersonDetails(int id);
    Task<Response<int>> CreatePerson(PersonViewModel person);
    Task<Response<int>> UpdatePerson(int id, PersonViewModel person);
    Task<Response<int>> DeletePerson(int id);
}