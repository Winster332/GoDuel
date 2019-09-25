using UnityEngine;

namespace Game
{
  public class ParallaxController : MonoBehaviour
  {
    public float Speed = 2;
    public float X => gameObject.transform.position.x;

    void Start()
    {
    }

    void Update()
    {
      gameObject.transform.Translate(Speed * Time.deltaTime, 0, 0);

      if (gameObject.transform.position.x >= 20)
        gameObject.transform.position =
          new Vector3(-20, gameObject.transform.position.y, gameObject.transform.position.z);
    }
  }
}
