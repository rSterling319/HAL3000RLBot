using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000
{
  public static class Utils
  {
    /// <summary>
    /// Returns a local location given either a Vec3 or GameObject and object 
    /// the target object will be local to.
    /// </summary>
    /// <param name="target_obj"></param>
    /// <param name="our_obj"></param>
    /// <returns></returns>
    public static Vec3 ToLocal(object target_obj, GameObject our_obj)
    {
      float x = (ToLocation(target_obj) - our_obj.Location) * our_obj.Matrix[0];
      float y = (ToLocation(target_obj) - our_obj.Location) * our_obj.Matrix[1];
      float z = (ToLocation(target_obj) - our_obj.Location) * our_obj.Matrix[2];

      return new Vec3(x, y, z);
    }

    /// <summary>
    /// Returns a Vec3 Location wether given a GameObject or Vec3.
    /// If neither, then a new Vec3.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Vec3 ToLocation(object target)
    {
      object vec3 = null;
      if (target is Vec3)
      {
        return (Vec3)target;
      }
      else if (target is GameObject)
      {
        return ((GameObject)target).Location;
      }
      else
      {
        return new Vec3();
      }
    }
  }
}
