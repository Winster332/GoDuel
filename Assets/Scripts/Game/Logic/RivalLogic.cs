using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Game;
using Game.Logic;
using UnityEngine;
using Random = System.Random;

public class RivalLogic : PersonAim
{
  public float TargetRandomAim;
  public long MillisecondsStart;
  public DateTime? DateTimeStart;
  public bool Enabled;
  
  void Start()
  {
    Enabled = true;
    DateTimeStart = null;
    var rand = new Random();
    MillisecondsStart = rand.Next(0, 1200);
    TypePerson = "rival";
    IsReverRotation = true;
    TargetRandomAim = rand.Next(60, 100) * -1;

    InitAim();
  }

  void Update()
  {
    if (IsPaused)
      return;
    
    if (Enabled)
    {
      if (RevertTimer.TouchEnabled)
      {
        if (DateTimeStart == null)
        {
          DateTimeStart = DateTime.Now.AddMilliseconds(MillisecondsStart);
        }
        else if (DateTime.Now >= DateTimeStart)
        {

          if (_isAim && RotationValue < TargetRandomAim)
          {
            _isAttack = true;
            _isAim = false;

            Attack();
            GameSession.Get().IsShotRival = true;
          }

          if (!_isAim && !_isAttack)
            _isAim = true;
        }

        if (_isAim)
          TakeAim();
      }
    }
  }
}
