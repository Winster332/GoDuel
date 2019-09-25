using System.Collections;
using System.Collections.Generic;
using Halper;
using Models.Managers;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
  public GameSchemeTimer Timers;
  
  void Start()
  {
    var gunsManager = GunManager.Get();
    var personsManager = PersonManager.Get();
    var profileManager = ProfileManager.Get();

    Timers = new GameSchemeTimer();
    Timers.AddTimer(3, gt =>
    {
      SceneManager.LoadScene("MenuScene");
    });
    Timers.StartAll();
  }

  void Update()
  {
    Timers.Update();
  }
}
