using HAL3000.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000.GameObjects
{
  public class Goal
  {
    public const float POST_X = 892.755f;
    public const float POST_Y = 5120.0f;
    public const float CROSSBAR_Z = 642.755f;
    public const float WIDTH = POST_X * 2.0f;

    public enum GoalType
    {
      Attacking = 0,
      Defending = 1,
    }
    private float _team = 0.0f;

    /// <summary>
    /// Blue = 0 Orange = 1
    /// So blue defending -y values, attacking +y values
    /// Orange defending +y values, attacking -y values
    /// </summary>
    /// <param name="team"></param>
    public Goal(int team, GoalType goalType)
    {

      switch (goalType)
      {
        case GoalType.Attacking:
          _team = -1.0f * Utils.Sign(team);
          break;
        case GoalType.Defending:
          _team = Utils.Sign(team);
          break;
      }

      InitializeGoal();
    }

    public Vec3 LeftPost { get; private set; }
    public Vec3 RightPost { get; private set; }
    public Vec3 Center { get; private set; }

    private void InitializeGoal()
    {
      LeftPost = new Vec3(-_team * POST_X, -_team * POST_Y, Ball.RADIUS);
      RightPost = new Vec3(_team * POST_X, -_team * POST_Y, Ball.RADIUS);
      Center = new Vec3(0.0, -_team * POST_Y, Ball.RADIUS);
    }
  }
}
