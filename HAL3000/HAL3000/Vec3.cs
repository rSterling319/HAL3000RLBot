using rlbot.flat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000
{
  public class Vec3
  {

    public Vec3()
    {

    }

    public Vec3(Vector3 value)
    {
      X = value.X;
      Y = value.Y;
      Z = value.Z;
    }

    public Vec3(Vec3 vec3)
    {
      X = vec3.X;
      Y = vec3.Y;
      Z = vec3.Z;
    }

    public Vec3(float x, float y, float z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    public float Magnitude()
    {
      return (float)Math.Sqrt(Math.Pow(X, 2.0) + Math.Pow(Y, 2.0) + Math.Pow(Z, 2.0));
    }

    public Vec3 Normalize()
    {
      float mag = Magnitude();
      if(mag != 0.0)
      {
        return new Vec3(X / mag, Y / mag, Z / mag);
      }
      else
      {
        return new Vec3(0.0f, 0.0f, 0.0f);
      }
    }

    #region operators

    /// <summary>
    /// Adds two Vec3s
    /// [x1+x2, y1+y2, z1+z2]
    /// </summary>
    /// <param name="vec1"></param>
    /// <param name="vec2"></param>
    /// <returns></returns>
    public static Vec3 operator +(Vec3 vec1, Vec3 vec2) => 
      new Vec3(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);

    /// <summary>
    /// Subtract two Vec3s
    /// [x1-x2, y1-y2, z1-z2]
    /// </summary>
    /// <param name="vec1"></param>
    /// <param name="vec2"></param>
    /// <returns>Subtracted vectors</returns>
    public static Vec3 operator -(Vec3 vec1, Vec3 vec2) =>
      new Vec3(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);

    /// <summary>
    /// Multiply 2 Vec3s
    /// The Dot product
    /// x1*x2 + y1*y2 + z1*z2]
    /// </summary>
    /// <param name="vec1"></param>
    /// <param name="vec2"></param>
    /// <returns></returns>
    public static float operator *(Vec3 vec1, Vec3 vec2) =>
      vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z;

    /// <summary>
    /// Multiplies the Vec3 by a scalar
    /// s * [x, y, z] =  [s*x, s*y, s*z]
    /// </summary>
    /// <param name="scalar"></param>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static Vec3 operator *(float scalar, Vec3 vec) =>
      new Vec3(scalar * vec.X, scalar * vec.Y, scalar * vec.Z);

    public static implicit operator Vec3(Vector3 vector3) => new Vec3(vector3);

    #endregion operators
  }
}
