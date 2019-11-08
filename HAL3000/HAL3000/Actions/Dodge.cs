using HAL3000.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000.Actions
{
  public class Dodge : Action
  {

    public Dodge(HAL3000 agent) : base(agent)
    {

    }

    public override void Step(float deltaTime)
    {
      float timeout = 0.9f;

      Controls.Jump = false;
      Controls.Roll = 0.0f;
      Controls.Pitch = 0.0f;
      Controls.Yaw = 0.0f;

      if(Duration.HasValue && _timer <= Duration.Value)
      {
        Controls.Jump = true;
      }

      float dodgeTime = 0.0f;

      if(!Duration.HasValue && !Delay.HasValue)
      {
        Debug.Assert(false, "Invalid dodge parameters");
      }
      if(!Duration.HasValue && Delay.HasValue)
      {
        dodgeTime = Delay.Value;
      }
      if(Duration.HasValue && !Delay.HasValue)
      {
        dodgeTime = Duration.Value + 2.0f * deltaTime;
      }
      if(Duration.HasValue && Delay.HasValue)
      {
        dodgeTime = Delay.Value;
      }



      if(_timer >= dodgeTime && !_agent.Me.DoubleJumped && !_agent.Me.HasWheelContact)
      {

        Vec3 directionLocal;

        if(Target == null)
        {
          directionLocal = new Vec3(0.0f, 0.0f, 0.0f);
        }
        else
        {
          directionLocal = (Target - _agent.Me.Location).Normalize();
        }
      }

    }
  }
}
