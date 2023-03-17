using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    [Required(ErrorMessage = "Don't they deserve a name too?")]
    public string Name { get; set; }
    [Required(ErrorMessage = "But what does it do though?")]
    public string Description { get; set; }
    [Required]
    public DateTime InstallDate { get; set; }
    public List<Repair> Repairs { get; set; }
  }
}