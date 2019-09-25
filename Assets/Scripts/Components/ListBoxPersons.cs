using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Halper;
using Models.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListBoxPersons : MonoBehaviour
{
  public Button BtnNext;
  public Button BtnBack;
  public Button BtnPlay;
  public Button BtnPay;
  public Image TargetImage;
  public Text Balance;
  public Text Title;
  public int CurrentViewPerson;

  void Start()
  {

    if (CultureInfo.InstalledUICulture.Name == "en-US")
    {
      BtnPlay.GetComponentInChildren<Text>().text = "Play";
      BtnPay.GetComponentInChildren<Text>().text = "Buy";
    }

    BtnNext.onClick.AddListener(BtnClickNext);
    BtnBack.onClick.AddListener(BtnClickBack);
    BtnPlay.onClick.AddListener(BtnClickPlay);
    BtnPay.onClick.AddListener(BtnClickPay);

    RefreshInformations();
  }

  public void RefreshInformations()
  {
    var profile = ProfileManager.Get().Profile;

    RefreshBalanceAndTitle();
    CurrentViewPerson = profile.CurrentPersonIndex;
    ProfileManager.Get().Profile.CurrentPersonIndex = CurrentViewPerson;
    ProfileManager.Get().Profile.GetPerson().index = CurrentViewPerson;
    ShowPerson();

    ShowTargetAvatar(CurrentViewPerson);
  }

  public void RefreshBalanceAndTitle()
  {
    var profile = ProfileManager.Get().Profile;
    
    Balance.text = profile.AmountCoint.ToString();
    Title.text = profile.GetPerson().name;
  }

  public void BtnClickNext() => ShowTargetAvatar(CurrentViewPerson+1);
  public void BtnClickBack() => ShowTargetAvatar(CurrentViewPerson-1);
  public void BtnClickPay()
  {
    var currentLot = PersonManager.Get().Persons.First(x => x.Value.index == CurrentViewPerson).Value;
    var currentBalance = ProfileManager.Get().Profile.AmountCoint;
    var currentPriceLot = currentLot.price;

    if (currentBalance >= currentPriceLot)
    {
      ProfileManager.Get().Profile.PersonAvialables.Add(currentLot.index);
      ProfileManager.Get().Profile.AmountCoint -= currentPriceLot;
      ProfileManager.Get().Profile.CurrentPersonIndex = currentLot.index;
      ProfileManager.Get().Commit();

      RefreshInformations();
    }
    else
    {
      // не хватило денег
    }
  }

  public void BtnClickPlay()
  {
    ProfileManager.Get().Profile.CurrentPersonIndex = CurrentViewPerson;
    ProfileManager.Get().Commit();
    
    SceneManager.LoadScene($"Level_{CurrentViewPerson}");
  }

  public void ShowTargetAvatar(int index)
  {
    if (index >= 1 && index <= PersonManager.Get().Persons.Count+1)
    {
      BtnBack.interactable = true;
      BtnNext.interactable = true;
      CurrentViewPerson = index;

      var activePerson = PersonManager.Get().Persons.First(x => x.Value.index == CurrentViewPerson).Value;

      TargetImage.sprite = activePerson.SpriteHead;
      BtnPay.GetComponentInChildren<Text>().text = activePerson.price.ToString();
      
      CheckOnPayment(activePerson);

      ShowPerson();
    }
    if (index <= 1)
    {
      BtnBack.interactable = false;
      BtnNext.interactable = true;
    } 
    if (index >= PersonManager.Get().Persons.Count)
    {
      BtnBack.interactable = true;
      BtnNext.interactable = false;
    }
  }

  public void CheckOnPayment(Person activePerson)
  {
    var isAvailableTargetPerson = ProfileManager.Get().Profile.PersonAvialables.Contains(activePerson.index);
    
    if (isAvailableTargetPerson)
    {
      BtnPlay.gameObject.SetActive(true);
      BtnPay.gameObject.SetActive(false);
      
      ProfileManager.Get().Profile.CurrentPersonIndex = activePerson.index;
      ProfileManager.Get().Profile.GetPerson().index = activePerson.index;
      RefreshBalanceAndTitle();
    }
    else
    {
      Title.text = "Не доступен";
      BtnPlay.gameObject.SetActive(false);
      BtnPay.gameObject.SetActive(true);
    }
  }

  public void ShowPerson()
  {
    var aPlayer = ProfileManager.Get().Profile.GetPerson();
    var circle = "circle";
    var box = "box";
    var z = -0.10f;
    var scale = 0.5f;

    PhysicsTransformer.SetSprite("player_head", aPlayer.SpriteHead, true, scale, circle, 0, 0, z);
    PhysicsTransformer.SetSprite("player_body", aPlayer.SpriteBody, true, scale, box, 0, 0, z);
    PhysicsTransformer.SetSprite("player_hand_top", aPlayer.SpriteHandTop, true, 0.6f, box, 0, 0, z);
    PhysicsTransformer.SetSprite("player_hand_bottom", aPlayer.SpriteHandBottom, true, 0.6f, box, 0, 0, z);
    PhysicsTransformer.SetSprite("player_leg_top", aPlayer.SpriteLegTop, true, scale);
    PhysicsTransformer.SetSprite("player_leg_bottom", aPlayer.SpriteLegBottom, true, scale);
    PhysicsTransformer.SetSprite("player_foot", aPlayer.SpriteFoot, true, scale, box);
    PhysicsTransformer.SetSprite("player_gun", aPlayer.GetGun().Sprite, false, 0.2f, box);

    PhysicsTransformer.SetAllSleep(GameObject.Find("person_player"),
      new[] {"player_head", "player_body", "player_leg_top", "player_leg_bottom", "player_foot"},
      RigidbodyConstraints2D.FreezeAll);
  }
}
