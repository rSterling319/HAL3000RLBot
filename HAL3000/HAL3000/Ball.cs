using rlbot.flat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000
{
  public class Ball : GameObject
  {
    public Ball() { }

    public Ball(BallInfo? ballInfo)
    {
      if(ballInfo != null)
      {
        Location = ballInfo.Value.Physics.Value.Location;
        Velocity = ballInfo.Value.Physics.Value.Velocity;
        Rotation = ballInfo.Value.Physics.Value.Rotation;
        AngularVelocity = ballInfo.Value.Physics.Value.AngularVelocity;
      }
    }
  }
}
