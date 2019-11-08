using HAL3000.GameObjects;
using HAL3000.Utility;
using rlbot.flat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HAL3000.GameObjects.Goal;

namespace HAL3000
{
  public class Player : GameObject
  {

    public Player() { }

    public Player(PlayerInfo? playerInfo)
    {
      if (playerInfo != null)
      {
        Location = playerInfo.Value.Physics.Value.Location;
        Velocity = playerInfo.Value.Physics.Value.Velocity;
        Rotation = playerInfo.Value.Physics.Value.Rotation;
        AngularVelocity = playerInfo.Value.Physics.Value.AngularVelocity;
        Boost = playerInfo.Value.Boost;
        Team = playerInfo.Value.Team <= 0 ? -1 : 1;
        HasWheelContact = playerInfo.Value.HasWheelContact;
        Jumped = playerInfo.Value.Jumped;
        DoubleJumped = playerInfo.Value.DoubleJumped;
        AttackingGoal = new Goal(Team, GoalType.Attacking);
        DefendingGoal = new Goal(Team, GoalType.Defending);
      }
    }

    public Vec3 Forward { get; }

    public Vec3 Left { get; }

    public Vec3 Up { get; }

    public float Boost { get; set; } = 0.0f;

    public int Team { get; set; } = 0;

    public bool HasWheelContact { get; set; } = false;

    public bool Jumped { get; set; } = false;

    public bool DoubleJumped { get; set; } = false;

    public Goal AttackingGoal { get; }

    public Goal DefendingGoal { get; }

    public float BallProject(Ball ball)
    {
      Vec3 goal = new Vec3(0.0f, (float)(Utils.Sign(Team) * Constants.FIELD_LENGTH / 2.0), 100.0f);
      Vec3 goalToBall = (ball.Location - goal).Normalize();
      Vec3 difference = Location - ball.Location;

      return difference * goalToBall;
    }

    public void Update(PlayerInfo? playerInfo) { }


  }
}
