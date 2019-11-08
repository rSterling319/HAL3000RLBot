using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL3000.Utility;
using RLBotDotNet;

namespace HAL3000.States
{
  public class ChasingState : State
  {

    public ChasingState() { }

    public override bool Available(HAL3000 agent)
    {
      return true;
    }

    public override Controller Execute(HAL3000 agent)
    {

      Vec3 targetLocation = new Vec3();
      float speed = 0.0f;

      targetLocation = agent.Ball.Location;

      //Get speed.
      Vec3 targetLocal = Utils.ToLocal(agent.Ball, agent.Me);
      double angleToTarget = Utils.Cap(Math.Atan2(targetLocal.Y, targetLocal.X), -3.0, 3.0);
      double distToTarget = agent.Me.DistanceTo2D(targetLocation);
      speed = (float)(2000.0 - (100.0 * Math.Pow((1.0 + angleToTarget), 2.0)));

      return agent.Controller(agent, targetLocation, speed);
    }

    private Controller ChasingController(HAL3000 agent, Vec3 targetLocation, float targetSpeed)
    {
      Controller controller = new Controller();

      Vec3 location = Utils.ToLocal(targetLocation, agent.Me);
      double angleToBall = MathCalc.AngleToTarget(agent.Ball, agent.Me);

      float currentSpeed = MathCalc.Velocity2D(agent.Me);
      controller.Steer = Utils.Steer(angleToBall);

      double timeDiff = Utils.MarkTime() - agent.Start;

      if(targetSpeed > currentSpeed)
      {
        controller.Throttle = 1.0f;
        if (agent.Me.DistanceTo2D(agent.Ball) > 300.0)
        {
          controller.Jump = true;
          controller.Pitch = -1.0f;
          agent.Start = Utils.MarkTime();
        }
        if(timeDiff > 1.0)
        {
          controller.Jump = true;
          controller.Pitch = -1.0f;
        }
      }
      else if (targetSpeed < currentSpeed)
      {
        controller.Throttle = 0.0f;
      }


      return controller;
    }
  }
}
