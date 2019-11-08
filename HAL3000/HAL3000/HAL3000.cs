using System;
using RLBotDotNet;
using rlbot.flat;
using System.Collections.Generic;
using HAL3000.States;
using HAL3000.Utility;

namespace HAL3000
{
  // We want to our bot to derive from Bot, and then implement its abstract methods.
  public class HAL3000 : Bot
  {    
    public State.Controller_State Controller;
    public Ball Ball = new Ball();
    public Player Me = new Player();
    private Dictionary<string, Player> _players = new Dictionary<string, Player>();
    public double Start = Utils.MarkTime();

    private State _state = new CalculatedShotState();

    private State CalcShotState { get; } = new CalculatedShotState();

    private State ChasingState { get; } = new ChasingState();

    // We want the constructor for ExampleBot to extend from Bot, but we don't want to add anything to it.
    public HAL3000(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
    {
    }


    public override Controller GetOutput(GameTickPacket gameTickPacket)
    {
      BallPrediction prediction = GetBallPrediction();
      

      // Loop through every 10th point so we don't render too many lines.
      for (int i = 10; i < prediction.SlicesLength; i += 10)
      {
        Vector3 pointA = prediction.Slices(i - 10).Value.Physics.Value.Location.Value;
        Vector3 pointB = prediction.Slices(i).Value.Physics.Value.Location.Value;

        Renderer.DrawLine3D(System.Windows.Media.Color.FromRgb(255, 0, 255), pointA, pointB);
      }


      PreProcess(gameTickPacket);
      CheckState();

      System.Numerics.Vector2 left = new System.Numerics.Vector2(10, 10 + 100 * team);
      Renderer.DrawString2D($"{team} - {MathCalc.AngleToTarget(Ball, Me).ToString("f2")}", 
                            System.Windows.Media.Color.FromRgb(255, 0, 255), left, 3, 3);

      return _state.Execute(this);
    }

    private void PreProcess(GameTickPacket game)
    {
      Ball = new Ball(game.Ball);
      Me = new Player(game.Players(this.index).Value);
      for(int i = 0; i < game.PlayersLength; ++i)
      {
        if(!_players.ContainsKey(game.Players(i).Value.Name))
        {
          _players.Add(game.Players(i).Value.Name, new Player(game.Players(i).Value));
        }
        else
        {
          _players[game.Players(i).Value.Name] = new Player(game.Players(i).Value);
        }
      }
    }

    private void CheckState()
    {
      if (_state.Expired)
      {
        if (CalcShotState.Available(this))
        {
          _state = CalcShotState;
        }
        if (ChasingState.Available(this))
        {
          _state = ChasingState;
        }
      }
    }
  }
}
