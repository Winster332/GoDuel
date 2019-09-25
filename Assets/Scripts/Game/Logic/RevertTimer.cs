using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Halper;
using UnityEngine;
using UnityEngine.UI;

public class RevertTimer : MonoBehaviour
{
  protected GameSchemeTimer SchemeTimer;
  protected Text TextGameObject;
  public Stack<string> StackMessages;
  public int TimerPauseSeconds = 2;
  public int PauseStartSeconds = 2;
  public float ScaleValue = -0.2f;
  public bool TouchEnabled = false;
  
  void Start()
  {
    StackMessages = new Stack<string>();
    
    if (CultureInfo.InstalledUICulture.Name == "en-US")
    {
      StackMessages.Push("Fire!");
    }
    else
    {
      StackMessages.Push("Огонь!");
    }

    StackMessages.Push("1");
    StackMessages.Push("2");
    StackMessages.Push("3");
    
    TextGameObject = GetComponent<Text>();
    TextGameObject.text = "";
    SchemeTimer = new GameSchemeTimer();
    SchemeTimer.AddTimer(PauseStartSeconds, gt => { SchemeTimer.Timers.Last().Start(); });
    SchemeTimer.AddTimer(TimerPauseSeconds, (gt) =>
    {

      if (StackMessages.Count > 0)
      {
        var msg = StackMessages.Pop();
        TextGameObject.text = msg;
        
        transform.localScale = new Vector3(1, 1, 0);
        SchemeTimer.Timers.Last().Start();
      }
      if (StackMessages.Count <= 0)
      {
        TouchEnabled = true;
      }
    });
    SchemeTimer.Timers.First().Start();
    
  }

  void Update()
  {
    if (transform.localScale.x > 0.01f)
    {
      transform.localScale += new Vector3(ScaleValue, ScaleValue, 0) * Time.deltaTime;
    }

    SchemeTimer.Update();
  }
}
