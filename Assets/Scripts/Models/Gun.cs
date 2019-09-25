using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Gun
{
  [NonSerialized] 
  public Sprite Sprite;

  public int index;
  public string name;
  public float power;
}
