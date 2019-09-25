using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models.Managers;
using UnityEngine;

[Serializable]
public class Person
{
  [NonSerialized] public Sprite SpriteHead;
  [NonSerialized] public Sprite SpriteBody;
  [NonSerialized] public Sprite SpriteHandTop;
  [NonSerialized] public Sprite SpriteHandBottom;
  [NonSerialized] public Sprite SpriteLegTop;
  [NonSerialized] public Sprite SpriteLegBottom;
  [NonSerialized] public Sprite SpriteFoot;

  public int index;
  public int gunIndex;
  public string name;
  public string description;
  public int price;

  public Gun GetGun()
  {
    return GunManager.Get().Guns.First(x => x.index == index);
  }
}
