using System;
using RLBotDotNet;
using rlbot.flat;

namespace HAL3000
{
  // We want to our bot to derive from Bot, and then implement its abstract methods.
  public class HAL3000 : Bot
  {
    private Controller _controllerState = new Controller();
    private GameDataManager _gameDataMgr = null;
    private Ball _ball = new Ball();
    private Player _me = new Player();
    private DateTime _start = DateTime.Now;

    //private State _state = some state
    //Controller? different from above?

    // We want the constructor for ExampleBot to extend from Bot, but we don't want to add anything to it.
    public HAL3000(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
    {
      _gameDataMgr = new GameDataManager();
    }

    public override Controller GetOutput(GameTickPacket gameTickPacket)
    {
      // This controller object will be returned at the end of the method.
      // This controller will contain all the inputs that we want the bot to perform.
      Controller controller = new Controller();

      _gameDataMgr.PreProcess(gameTickPacket);

      // Store the required data from the gameTickPacket.
      // The GameTickPacket's attributes are nullables, so you must use .Value.
      // It is recommended to create your own internal data structure to avoid the displeasing .Value syntax.
      Vector3 ballLocation = gameTickPacket.Ball.Value.Physics.Value.Location.Value;
      Vector3 carLocation = gameTickPacket.Players(this.index).Value.Physics.Value.Location.Value;
      Rotator carRotation = gameTickPacket.Players(this.index).Value.Physics.Value.Rotation.Value;

      // Calculate to get the angle from the front of the bot's car to the ball.
      double botToTargetAngle = Math.Atan2(ballLocation.Y - carLocation.Y, ballLocation.X - carLocation.X);
      double botFrontToTargetAngle = botToTargetAngle - carRotation.Yaw;
      // Correct the angle
      if (botFrontToTargetAngle < -Math.PI)
        botFrontToTargetAngle += 2 * Math.PI;
      if (botFrontToTargetAngle > Math.PI)
        botFrontToTargetAngle -= 2 * Math.PI;

      // Decide which way to steer in order to get to the ball.
      if (botFrontToTargetAngle > 0)
        controller.Steer = 1;
      else
        controller.Steer = -1;

      // Set the throttle to 1 so the bot can move.
      controller.Throttle = 1;

      return controller;
    }
  }
}
