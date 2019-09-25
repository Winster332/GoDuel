using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenuController : MonoBehaviour
{
  public bool IsShow;
  public Button _btnClose;
  public Button _btnRestart;
  public Button _btnHome;
  public Button _btnContine;
  public Button _btnPause;
  public PlayerLogic PlayerLogic;
  public RivalLogic RivalLogic;
  
  void Start()
  {
    IsShow = false;
    ChangeElementView();

    UseListenersForButtons();
  }

  private void UseListenersForButtons()
  {
    _btnPause.onClick.AddListener(Show);
    _btnClose.onClick.AddListener(Hide);
    _btnContine.onClick.AddListener(Hide);
    _btnHome.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));
  }

  public void Show()
  {
    IsShow = true;
    PlayerLogic.IsPaused = true;
    RivalLogic.IsPaused = true;
    ChangeElementView();
  }
  
  public void Hide()
  {
    IsShow = false;
    PlayerLogic.IsPaused = false;
    RivalLogic.IsPaused = false;
    ChangeElementView();
  }

  private void ChangeElementView()
  {
    gameObject.SetActive(IsShow);
  }

  void Update()
  {

  }
}
