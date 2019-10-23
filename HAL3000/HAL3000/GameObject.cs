using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000
{
  public class GameObject
  {
    private Vec3[] _matrix = null;

    public GameObject()
    {

    }

    public Vec3 Location { get; set; } = new Vec3(0.0f, 0.0f, 0.0f);

    public Vec3 Velocity { get; set; } = new Vec3(0.0f, 0.0f, 0.0f);

    public Rotator3 Rotation { get; set; } = new Rotator3(0.0f, 0.0f, 0.0f);

    public Vec3 AngularVelocity { get; set; } = new Vec3(0.0f, 0.0f, 0.0f);

    public Vec3 LocalLocation { get; set; } = new Vec3(0.0f, 0.0f, 0.0f);

    public Vec3[] Matrix
    {
      get
      {
        if(_matrix == null)
        {
          _matrix = RotatorToMatrix();
        }

        return _matrix;
      }
    }




    private Vec3[] RotatorToMatrix()
    {
      float cosRoll = (float)Math.Cos(Rotation.Roll);
      float sinRoll = (float)Math.Sin(Rotation.Roll);
      float cosPitch = (float)Math.Cos(Rotation.Pitch);
      float sinPitch = (float)Math.Sin(Rotation.Pitch);
      float cosYaw = (float)Math.Cos(Rotation.Yaw);
      float sinYaw = (float)Math.Sin(Rotation.Yaw);

      Vec3[] matrix = new Vec3[3];
      matrix[0] = new Vec3(cosPitch * cosYaw, cosPitch * sinYaw, sinPitch);
      matrix[1] = new Vec3(cosYaw * sinPitch * sinRoll - cosRoll * sinYaw, sinYaw * sinPitch * sinRoll + cosRoll * cosYaw, -cosPitch * sinRoll);
      matrix[2] = new Vec3(-cosRoll * cosYaw * sinPitch - sinRoll * sinYaw, -cosRoll * sinYaw * sinPitch + sinRoll * cosYaw, cosPitch * cosRoll);

      return matrix;
    }

  }
}
