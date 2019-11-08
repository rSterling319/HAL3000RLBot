using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL3000.Utility;
using RLBotDotNet;

namespace HAL3000.States
{
  public class Dodge : State
  {
    public override bool Available(HAL3000 agent)
    {
      return Expired;
    }

    public override Controller Execute(HAL3000 agent)
    {
      Vec3 target = agent.Ball.Location;
      
    }

    public Controller DodgeController(HAL3000 agent, Vec3 targetLocation, float targetSpeed)
    {
      Controller controller = new Controller();
      float timeout = 0.9f;
      controller.Jump = false;
      controller.Roll = 0.0f;
      controller.Pitch = 0.0f;
      controller.Yaw = 0.0f;
    }
  }
}
