using HAL3000.Utility;
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
    public const float RADIUS = 92.75f;
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

    public bool Ready()
    {
      return Math.Abs(Velocity.Z) < 150.0 && TimeZ() < 1.0;
    }

    public double TimeZ()
    {
      double rate = 0.97;
      return MathCalc.QuadEq(-325.0, Velocity.Z * rate, Location.Z - RADIUS);
    }
  }
}
