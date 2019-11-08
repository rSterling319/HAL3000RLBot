using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000.Utility
{
  public static class MathCalc
  {

    public static double QuadEq(double a, double b, double c)
    {
      double fourAc = 4.0f * a * c;
      double b2 = (float)Math.Pow(b, 2.0f);

      double inside = b2 - fourAc;

      if(inside < 0 || a == 0)
      {
        return 0.1f;
      }

      double neg = (-b - Math.Sqrt(inside)) / (2 * a);
      double pos = (-b + Math.Sqrt(inside)) / (2 * a);

      return pos > neg ? pos : neg;
    }

    public static double Dpp(Vec3 targetLocation, Vec3 targetVelocity, Vec3 sourceLocation, Vec3 sourceVelocity)
    {
      double dist = Distance2D(targetLocation, targetVelocity);

      double locXVelX = (targetLocation.X - sourceLocation.X) * (targetVelocity.X - sourceVelocity.X);
      double locXVelY = (targetLocation.Y - sourceLocation.Y) * (targetVelocity.Y - sourceVelocity.Y);
      double locXVelZ = (targetLocation.Z - sourceLocation.Z) * (targetVelocity.Z - sourceVelocity.Z);

      return dist == 0.0 ? 0.0 : (locXVelX + locXVelY + locXVelZ);
    }


    public static double Distance2D(GameObject target, Vec3 source)
    {
      return Distance2D(target.Location, source);
    }

    public static double Distance2D(Vec3 target, GameObject source)
    {
      return Distance2D(target, source.Location);
    }

    public static double Distance2D(GameObject target, GameObject source)
    {
      return Distance2D(target.Location, source.Location);
    }

    public static double Distance2D(Vec3 target, Vec3 source)
    {
      Vec3 diff = target - source;
      return Math.Sqrt(diff.Xp2 + diff.Yp2);
    }

    public static float Velocity2D(GameObject gameObject)
    {
      return (float)Math.Sqrt(gameObject.Velocity.Xp2 + gameObject.Velocity.Yp2);
    }

    public static double AngleToTarget(GameObject target, GameObject source)
    {
      return AngleToTarget(target.Location, source.Location);
    }

    public static double AngleToTarget(Vec3 target, GameObject source)
    {
      return AngleToTarget(target, source.Location);
    }

    public static double AngleToTarget(GameObject target, Vec3 source)
    {
      return AngleToTarget(target.Location, source);
    }

    public static double AngleToTarget(Vec3 target, Vec3 source)
    {
      Vec3 diff = target - source;
      return Math.Atan2(diff.Y, diff.X);
    }
  }
}
