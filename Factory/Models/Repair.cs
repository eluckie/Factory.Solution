using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Repair
  {
    public int RepairId { get; set; }
    [Range(1, int.MaxValue)]
    public int EngineerId { get; set; }
    public Engineer Engineer { get; set; }
    [Range(1, int.MaxValue)]
    public int MachineId { get; set; }
    public Machine Machine { get; set; }
  }
}