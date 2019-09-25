using System.Linq;
using UnityEngine;

namespace Halper
{
  public class PhysicsTransformer
  {
    public static void SetAllSleep(GameObject gObject, string[] names, RigidbodyConstraints2D constraints2D)
    {
      var rbs = gObject.GetComponentsInChildren<Rigidbody2D>().Where(x => names.Contains(x.name)).ToList();
      rbs.ForEach(x => x.constraints = constraints2D);
    }

    public static void SetSprite(string name, Sprite sprite, bool flipX = false, float scale = -1, string type = "box",
      float offsetX = 0, float offsetY = 0, float offsetZ = 0)
    {
      var gameObject = GameObject.Find(name);
      var objectTransform = gameObject.GetComponent<Transform>();
      objectTransform.Translate(offsetX, offsetY, offsetZ);
      gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

      if (scale >= 0)
      {
        objectTransform.localScale = new Vector3(scale, scale, scale);
      }
      else
      {
        scale = objectTransform.localScale.x;
      }

      if (flipX)
      {
        objectTransform.localScale = new Vector3(scale * -1, scale, scale);
      }

      if (type == "circle")
      {
        gameObject.AddComponent<CircleCollider2D>();
      }
      else if (type == "box")
      {
        gameObject.AddComponent<BoxCollider2D>();
      }
    }
  }
}
