using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime InstallDate { get; set; }
    List<Repair> Repairs { get; set; }
  }
}