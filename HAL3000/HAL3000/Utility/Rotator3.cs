using rlbot.flat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000.Utility
{
  public class Rotator3
  {

    public Rotator3() { }

    public Rotator3(Rotator rotator)
    {
      Pitch = rotator.Pitch;
      Yaw = rotator.Yaw;
      Roll = rotator.Roll;
    }

    public Rotator3(Rotator3 rotator)
    {
      Pitch = rotator.Pitch;
      Yaw = rotator.Yaw;
      Roll = rotator.Roll;
    }

    public Rotator3(float pitch, float yaw, float roll)
    {
      Pitch = pitch;
      Yaw = yaw;
      Roll = roll;
    }

    public float Pitch { get; set; } = 0.0f;

    public float Yaw { get; set; } = 0.0f;

    public float Roll { get; set; } = 0.0f;

    public static implicit operator Rotator3(Rotator rtr) => new Rotator3(rtr);
  }
}
