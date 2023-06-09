using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    [Required(ErrorMessage = "Speak their name!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Don't forget the details!")]
    public string Pronouns { get; set; }
    [Required]
    public DateTime HireDate { get; set; }
    public List<Repair> Repairs { get; set; }
  }
}