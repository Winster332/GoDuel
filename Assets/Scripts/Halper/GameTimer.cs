using System;
using System.Collections.Generic;

namespace Halper
{
  public class GameSchemeTimer
  {
    public List<GameTimer> Timers { get; set; }

    public GameSchemeTimer()
    {
      Timers = new List<GameTimer>();
    }

    public GameSchemeTimer AddTimer(int seconds, Action<GameTimer> callback)
    {
      var timer = GameTimer.Create(seconds, callback);
      Timers.Add(timer);
      
      return this;
    }

    public void Update()
    {
      Timers.ForEach(timer => timer.Update());
    }

    public GameSchemeTimer StartAll()
    {
      Timers.ForEach(timer => timer.Start());

      return this;
    }
  }

  public class GameTimer
  {
    public event EventHandler<int> Complated;
    public int Seconds { get; set; }
    private DateTime _targetTime;
    private bool _isComplated;
    private Action<GameTimer> _callBackAction;

    private GameTimer()
    {
    }

    public static GameTimer Create(int seconds, Action<GameTimer> callback = null)
    {
      var timer = new GameTimer
      {
        _callBackAction = callback,
        Seconds = seconds,
        _isComplated = false,
        _targetTime = DateTime.Now
      };

      return timer;
    }

    public GameTimer Start()
    {
      _targetTime = DateTime.Now.AddSeconds(Seconds);
      _isComplated = true;
      return this;
    }

    public void Update()
    {
      if (DateTime.Now >= _targetTime && _isComplated)
      {
        _isComplated = false;
        Complated?.Invoke(this, Seconds);
        _callBackAction?.Invoke(this);
      }
    }
  }
}