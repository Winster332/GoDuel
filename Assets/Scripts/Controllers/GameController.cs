using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Game;
using GoogleMobileAds.Api;
using Halper;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  public GameSchemeTimer SchemeTimer;
  public Button BtnPause;
  public Button BtnHomeIfWind;
  public Button BtnHomeIfLose;
  public Button BtnRestartIfWind;
  public Button BtnRestartIfLose;
  public Text Balance;
  public bool IsEnd;
  public GameObject WinPanel;
  public GameObject LosePanel;
  public const string AdBannerText = "ca-app-pub-3702545222621633/4010614540";
  public const string AdBannerVideo = "ca-app-pub-3702545222621633/8804486660";

  void Start()
  {
    BtnPause.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));
    BtnHomeIfLose.onClick.AddListener(ToHome);
    BtnHomeIfWind.onClick.AddListener(ToHome);
    BtnRestartIfLose.onClick.AddListener(ToGame);
    BtnRestartIfWind.onClick.AddListener(ToGame);


    if (CultureInfo.InstalledUICulture.Name == "en-US")
    {
      BtnHomeIfLose.GetComponentInChildren<Text>().text = "Home";
      BtnHomeIfWind.GetComponentInChildren<Text>().text = "Home";
      BtnRestartIfLose.GetComponentInChildren<Text>().text = "Restart";
      BtnRestartIfWind.GetComponentInChildren<Text>().text = "Restart";
    }

    IsEnd = false;
    GameSession.Get().Begin();
    
    SchemeTimer = new GameSchemeTimer();
    SchemeTimer.AddTimer(2, gt =>
    {
      if (GameSession.Get().IsWin())
      {
        ProfileManager.Get().Profile.AmountCoint += 300;
        ProfileManager.Get().Commit();

        WinPanel.SetActive(true);
        
        if (CultureInfo.InstalledUICulture.Name == "en-US")
        {
          WinPanel.GetComponentsInChildren<Text>().First().text = "Win";
          GameObject.Find("Text (1)").GetComponent<Text>().text = "+300";
        }
      }
      else
      {
        ProfileManager.Get().Profile.AmountCoint += 100;
        ProfileManager.Get().Commit();
        
        LosePanel.SetActive(true);
        
        if (CultureInfo.InstalledUICulture.Name == "en-US")
        {
          LosePanel.GetComponentsInChildren<Text>().First().text = "Lost";
          GameObject.Find("Text (1)").GetComponent<Text>().text = "+100";
        }
      }
    });

    MobileAds.Initialize(AdBannerText);
    var banner = new BannerView(AdBannerText, AdSize.Banner, AdPosition.Bottom);
    var request = new AdRequest.Builder().Build();
    banner.LoadAd(request);

    banner.Show();
  }

  private void ToHome()
  {
    SceneManager.LoadScene("MenuScene");
  }

  private void ToGame()
  {
    SceneManager.LoadScene($"Level_{ProfileManager.Get().Profile.CurrentPersonIndex}");
  }

  void Update()
  {
    Balance.text = ProfileManager.Get().Profile.AmountCoint.ToString();

    if (GameSession.Get().IsGameEnd() && !IsEnd)
    {
      SchemeTimer.StartAll();
      IsEnd = true;
    }

    SchemeTimer.Update();
  }
}
