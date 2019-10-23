using rlbot.flat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000
{
  public class Player : GameObject
  {

    public Player() { }

    public Player(PlayerInfo? playerInfo)
    {
      if(playerInfo != null)
      {
        Location = playerInfo.Value.Physics.Value.Location;
        Velocity = playerInfo.Value.Physics.Value.Velocity;
        Rotation = playerInfo.Value.Physics.Value.Rotation;
        AngularVelocity = playerInfo.Value.Physics.Value.AngularVelocity;
        Boost = playerInfo.Value.Boost;
      }
    }



    public float Boost { get; set; } = 0.0f;
  }
}
