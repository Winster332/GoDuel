using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Halper;
using Models.Managers;
using UnityEngine;

public class RivalPhysicsTransformer : MonoBehaviour
{
  void Start()
  {
    InitRival();
  }

  public void InitRival()
  {
    var aRival = PersonManager.Get().Persons[3];
    var circle = "circle";
    var box = "box";
    var z = -0.10f;
    var scale = 0.5f;

    PhysicsTransformer.SetSprite("rival_head", aRival.SpriteHead, true, scale, circle, 0, 0, z);
    PhysicsTransformer.SetSprite("rival_body", aRival.SpriteBody, true, scale, box, 0, 0, z);
    PhysicsTransformer.SetSprite("rival_hand_top", aRival.SpriteHandTop, true, 0.6f, box, 0, 0, z);
    PhysicsTransformer.SetSprite("rival_hand_bottom", aRival.SpriteHandBottom, true, 0.6f, box, 0, 0, z);
    PhysicsTransformer.SetSprite("rival_leg_top", aRival.SpriteLegTop, true, scale);
    PhysicsTransformer.SetSprite("rival_leg_bottom", aRival.SpriteLegBottom, true, scale);
    PhysicsTransformer.SetSprite("rival_foot", aRival.SpriteFoot, true, scale, box);
    PhysicsTransformer.SetSprite("rival_gun", aRival.GetGun().Sprite, false, 0.2f, box);

    PhysicsTransformer.SetAllSleep(GameObject.Find("person_rival"),
      new[] {"rival_head", "rival_body", "rival_leg_top", "rival_leg_bottom", "rival_foot"},
      RigidbodyConstraints2D.FreezeAll);
  }

  void Update()
  {

  }
}
