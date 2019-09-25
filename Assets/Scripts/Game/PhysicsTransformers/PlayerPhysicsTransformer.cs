using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Halper;
using Models.Managers;
using UnityEngine;

public class PlayerPhysicsTransformer : MonoBehaviour
{
  public bool AutoTransformed = true;
  void Start()
  {
    if (isActiveAndEnabled)
    {
      InitRival();
    }
  }
  
  public void InitRival()
  {
    if (isActiveAndEnabled)
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

  void Update()
  {

  }
}
