using System;
using System.Linq;
using UnityEngine;

namespace Game.Logic
{
  public class PersonAim : MonoBehaviour
  {
    public string TypePerson;
    public bool _isAim;
    public float Speed;
    public bool IsReverRotation = false;
    public float SpeedMax = 600;
    public float CurrentVelocity;
    public bool _isAttack;
    public Vector2 VectorAim;
    public float StartRotationGun;
    public Sprite SpriteBullet;
    protected GameObject PlayerHeader;
    protected RevertTimer RevertTimer;
    public float RotationValue;
    protected AudioSource AudioShot;
    public bool IsPaused { get; set; }


    protected void InitAim()
    {
      IsPaused = false;
      AudioShot = GameObject.Find("AudioShot").GetComponent<AudioSource>();
      RevertTimer = GameObject.Find("RevertTimerText").GetComponent<RevertTimer>();
      StartRotationGun = 0;
      _isAim = false;
      _isAttack = false;
    }

    protected void Attack()
    {
      GenerateBullet();
      
      AudioShot.Play();
    }

    protected void GenerateBullet()
    {
      var gunCollider = GetComponentsInChildren<BoxCollider2D>().FirstOrDefault(x => x.name == $"{TypePerson}_gun");
      var gunRigidBody = GetComponentsInChildren<Rigidbody2D>().FirstOrDefault(x => x.name == $"{TypePerson}_gun");
//    var bodies = hand.GetComponentsInChildren<Rigidbody2D>().Select(x => x).ToList();
      var gunSize = gunCollider.size;
      var gunPosition = gunRigidBody.position;

      Debug.Log("END");
      var gameObject = new GameObject("bullet");
      gameObject.AddComponent<SpriteRenderer>();
      gameObject.GetComponent<SpriteRenderer>().sprite =
        SpriteBullet; //Sprite.Create(Texture2D.whiteTexture, new Rect(-5, -5, 5, 5), new Vector2());
      gameObject.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
//    gameObject.GetComponent<SpriteRenderer>().size = new Vector2(10, 10);
//    
      gameObject.AddComponent<BoxCollider2D>();
      gameObject.AddComponent<Rigidbody2D>();
      gameObject.transform.position = gunPosition;
      var angle = ToRadian(gunRigidBody.rotation + StartRotationGun);
      VectorAim = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

      if (IsReverRotation)
        VectorAim *= -1;
      
      gameObject.GetComponent<Rigidbody2D>().AddForce(VectorAim * 3000, ForceMode2D.Force);
      gameObject.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
      gameObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
      gameObject.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
      gameObject.AddComponent<BulletLogic>();

//    Instantiate(gameObject);
//    return gameObject.GetComponent<Rigidbody2D>();
    }

    protected float ToRadian(float value)
    {
      return value / 180.0f * (float) Math.PI;
    }

    protected void SetAllSleep()
    {
      var rbs = GetComponentsInChildren<Rigidbody2D>().Where(x =>
          x.name != $"{TypePerson}_hand_top" && x.name != $"{TypePerson}_hand_bottom" && x.name != $"{TypePerson}_gun")
        .ToList();
      rbs.ForEach(x => x.constraints = RigidbodyConstraints2D.FreezeAll);
    }

    protected void TakeAim()
    {
      var hand = GetComponentsInChildren<Rigidbody2D>().First(b => b.name == $"{TypePerson}_hand_top");

      if (hand.angularVelocity <= SpeedMax)
      {
        if (!IsReverRotation)
        {
          hand.angularVelocity += Speed;
        }
        else
        {
          hand.angularVelocity += Speed * -1;
          RotationValue = hand.rotation;
        }

        CurrentVelocity = hand.angularVelocity;
      }
    }
  }
}
