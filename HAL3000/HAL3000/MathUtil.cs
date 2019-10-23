using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000
{
  public static class MathUtil
  {

    public static double AngleToTarget(Vec3 from, Vec3 target)
    {
      return Math.Atan2(target.Y - from.Y, target.X - from.X);
    }
  }
}
