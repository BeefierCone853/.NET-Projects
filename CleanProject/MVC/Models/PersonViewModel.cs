using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class PersonViewModel
{
    public int Id { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string CollegeName { get; set; }
}