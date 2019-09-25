using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
  private Button _button;
  
  void Start()
  {
    _button = GameObject.Find("ButtonLogin").GetComponent<Button>();
    _button.onClick.AddListener(() =>
    {
      var login = GameObject.Find("TextInputLogin").GetComponent<Text>().text;
      var pass = Guid.NewGuid().ToString();

      var regResult = RestApi.Get().Registration(login, pass).GetAwaiter().GetResult();
      var regLogin = RestApi.Get().Login(login, pass).GetAwaiter().GetResult();
    });
  }

  void Update()
  {

  }
}
