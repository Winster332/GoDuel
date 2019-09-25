using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Game.Logic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : PersonAim
{
  public Button ButtonPause;
  void Start()
  {
    TypePerson = "player";
    
    InitAim();
  }

  void Update()
  {
    if (IsPaused)
      return;
    
    if (Input.GetMouseButtonDown(0) && RevertTimer.TouchEnabled)
    {
      Debug.Log("Start");

      var rect = ButtonPause.GetComponent<RectTransform>();

      var mY = Screen.height - Input.mousePosition.y;
      if (mY > 60.0f)
      {
        if (_isAim)
        {
          _isAttack = true;
          _isAim = false;

          Attack();
          GameSession.Get().IsShotPlayer = true;
        }

        if (!_isAim && !_isAttack)
          _isAim = true;
      }
    }
    if (_isAim)
      TakeAim();
  }
}
