using HAL3000.Utility;
using RLBotDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL3000.Actions
{
  public abstract class Action
  {

    protected float _timer;
    protected float _timeout;

    protected HAL3000 _agent;

    public Action(HAL3000 agent)
    {
      _agent = agent;
      Target = new Vec3();
      Finished = false;
      _timer = 0.0f;

      Controls = new Controller();
    }

    public Vec3 Target { get; set; }

    public float? Duration { get; set; }
    public float? Delay { get; set; }

    public Controller Controls;

    public bool Finished { get; protected set; }

    public abstract void Step(float deltaTime);
  }
}
