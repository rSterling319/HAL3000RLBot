using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL3000.Utility;
using RLBotDotNet;

namespace HAL3000.States
{
  public class CalculatedShotState : State
  {
    public CalculatedShotState() { }

    public override bool Available(HAL3000 agent)
    {
      return agent.Ball.Ready() && Math.Abs(agent.Ball.Location.Y) < 5050.0 && agent.Me.BallProject(agent.Ball) > (500.0 - agent.Me.DistanceTo2D(agent.Ball));
    }

    public override Controller Execute(HAL3000 agent)
    {
      agent.Controller = CalcController;
      Vec3 targetLocation = new Vec3();
      float speed = 0.0f;


      float timeGuess = 0.0f;
      Vec3 bloc = Utils.Future(agent.Ball, timeGuess);

      //Angles from the goal posts to the ball and to agent.
      double ballLeft = MathCalc.AngleToTarget(bloc, agent.Me.AttackingGoal.LeftPost);
      double ballRight = MathCalc.AngleToTarget(bloc, agent.Me.AttackingGoal.RightPost);
      double agentLeft = MathCalc.AngleToTarget(agent.Me, agent.Me.AttackingGoal.LeftPost);
      double agentRight = MathCalc.AngleToTarget(agent.Me, agent.Me.AttackingGoal.RightPost);

      Vec3 goalTarget = null;
      //Determine if left/right/inside of cone
      if(agentLeft > ballLeft && agentRight > ballRight)
      {
        goalTarget = agent.Me.AttackingGoal.RightPost;
      }
      else if(agentLeft < ballLeft && agentRight < ballRight)
      {
        goalTarget = agent.Me.AttackingGoal.LeftPost;
      }
      //Otherwise in cone, remain null

      Vec3 goalToBall = null;
      Vec3 goalToAgenet = null;
      double error = 0.0;

      //If outside cone
      if(goalTarget != null)
      {
        goalToBall = (agent.Ball.Location - goalTarget).Normalize();
        goalToAgenet = (agent.Me.Location - goalTarget).Normalize();
        Vec3 diff = goalToBall - goalToAgenet;
        error = Utils.Cap(Math.Abs(diff.X) + Math.Abs(diff.Y), 1.0, 10.0);
      }
      //if inside the cone,
      else
      {
        goalToBall = (agent.Me.Location - agent.Ball.Location).Normalize();
        error = Utils.Cap(MathCalc.Distance2D(bloc, agent.Me) / 1000.0, 0.0, 1.0);
      }

      //Measure how fast ball is traveling away if stationary
      double ballDppSkew = Utils.Cap(Math.Abs(MathCalc.Dpp(agent.Ball.Location, agent.Ball.Velocity, agent.Me.Location, new Vec3())) / 80.0, 1.0, 1.5);

      double targetDistance = Utils.Cap((40.0 + MathCalc.Distance2D(agent.Ball, agent.Me) * Math.Pow(error, 2.0)) / 1.8, 0.0, 4000.0);
      targetLocation = agent.Ball.Location + new Vec3(goalToBall.X * targetDistance * ballDppSkew, goalToBall.Y * targetDistance, 0.0);

      //Adjust Target Location based on dpp
      double ballAdj = Math.Pow(MathCalc.Dpp(targetLocation, agent.Ball.Velocity, agent.Me.Location, new Vec3()), 2.0);
      
      //If stopped, and ball is moving 100uu away
      if(ballAdj > 100.0)
      {
        ballAdj = Utils.Cap(ballAdj, 0.0, 80.0);
        Vec3 correction = agent.Ball.Velocity.Normalize();
        correction = ballAdj * correction;

        targetLocation += correction;
      }

      //If ball is close to wall
      if(4120.0 - Math.Abs(targetLocation.X) < 0.0)
      {
        targetLocation.X = (float)Utils.Cap(targetLocation.X, -4120.0, 4120.0);
        targetLocation.Y = targetLocation.Y + (-Utils.Sign(agent.team) * (float)Utils.Cap(4120.0 - Math.Abs(targetLocation.X), -500, 500));
      }

      //Get speed.
      Vec3 targetLocal = Utils.ToLocal(agent.Ball, agent.Me);
      double angleToTarget = Utils.Cap(Math.Atan2(targetLocal.Y, targetLocal.X), -3.0, 3.0);
      double distToTarget = agent.Me.DistanceTo2D(targetLocation);
      speed = (float)(2000.0 - (100.0 * Math.Pow((1.0 + angleToTarget), 2.0)));


      

      return agent.Controller(agent, targetLocation, speed);
    }

    private Controller CalcController(HAL3000 agent, Vec3 targetLocation, float targetSpeed) 
    {
      Controller controller = new Controller();
      Vec3 location = Utils.ToLocal(targetLocation, agent.Me);
      double angleToBall = Math.Atan2(location.Y, location.X);

      float currentSpeed = MathCalc.Velocity2D(agent.Me);
      controller.Steer = Utils.Steer(angleToBall);
      if(Math.Abs(angleToBall) > Math.PI / 2.0 && Math.Abs(angleToBall) < Math.PI)
      {
        controller.Handbrake = true;
      }

      double timeDiff = Utils.MarkTime() - agent.Start;

      //Throttle
      if (targetSpeed > currentSpeed)
      {
        controller.Throttle = 1.0f;
        if (targetSpeed > 1400.0 && timeDiff > 2.2 && currentSpeed < 2250.0)
        {
          controller.Boost = true;
        }
        agent.Start = Utils.MarkTime();
      }
      else if (targetSpeed < currentSpeed)
      {
        controller.Throttle = -1.0f;
      }

      if(targetLocation.Z > 92.75 * 2.0)
      {
        controller.Throttle = 0.3f;
      }

      return controller;
    }
  }
}
