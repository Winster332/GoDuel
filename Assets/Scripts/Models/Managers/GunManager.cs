using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Halper;
using UnityEngine;

namespace Models.Managers
{
  public class GunManager
  {
    private string _fileName = "Guns";
    private static GunManager _instance;
    public static GunManager Get() => _instance ?? (_instance = new GunManager());
    public List<Gun> Guns { get; set; }

    private GunManager()
    {
      if (CultureInfo.InstalledUICulture.Name == "en-US")
        Guns = JsonHelper.FromJson<Gun>(Resources.Load("Metadata/GunsInfo_en").ToString()).ToList();
      else Guns = JsonHelper.FromJson<Gun>(Resources.Load("Metadata/GunsInfo_ru").ToString()).ToList();
      
      var gunsSprites = Resources.LoadAll<Sprite>(_fileName);
      Guns.ForEach(gun =>
      {
        foreach (var gunSprite in gunsSprites)
        {
          var spriteIndex = gunSprite.name.Replace("Gun_", "");
          if (gun.index == int.Parse(spriteIndex))
          {
            gun.Sprite = gunSprite;
          }
        }
      });
    }
  }
}