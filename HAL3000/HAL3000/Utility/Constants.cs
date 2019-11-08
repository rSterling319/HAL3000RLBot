using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000.Utility
{
  public static class Constants
  {
    public const float SIDEWALL_X = 4096.0f;
    public const float BACKWALL_Y = 5120.0f;
    public const float FLOOR_Z = 0.0f;
    public const float CEILING_Z = 2044.0f;
    public const float FIELD_LENGTH = 2.0f * BACKWALL_Y;
    public const float FIELD_WIDTH = 2.0f * SIDEWALL_X;
  }
}
