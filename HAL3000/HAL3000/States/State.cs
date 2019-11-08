using HAL3000.Utility;
using RLBotDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000.States
{
  public abstract class State
  {

    public enum Status
    {
      RESET = 0,
      WAIT = 1,
      INITIALIZE = 2,
      RUNNING = 3,
    }

    public delegate Controller Controller_State(HAL3000 agent, Vec3 targetLocation, float speed);

    public bool Expired { get; set; } = false;

    public abstract bool Available(HAL3000 agent);

    public abstract Controller Execute(HAL3000 agent);
  }
}
