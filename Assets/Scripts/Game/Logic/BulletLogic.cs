using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEditor;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
  protected RevertTimer RevertTimer;
  
  private void OnCollisionEnter2D(Collision2D other)
  {
    var args = other.gameObject.name.Split('_');
    if (args.Length == 0)
      return;
    var name = args.First();
    if (name == "ground")
    {
      Debug.Log("Ground");
      
    }
    
    var bullet = GameObject.Find("bullet");

    if (name == "rival")
    {
      Debug.Log("Rival");
      SetAllSleep(GameObject.Find("person_rival"), new[]
      {
        "rival_head", "rival_body", "rival_leg_top", "rival_leg_bottom", "rival_foot"
      }, RigidbodyConstraints2D.None);
      GameSession.Get().IsPlayerWin = true;
      
      GenerateBlood(other, bullet);
      GenerateBloodParticles(other);

      var gameObjectRival = GameObject.Find("person_rival");
      gameObjectRival.GetComponent<RivalLogic>().Enabled = false;
      GameObject.Find("Main Camera").GetComponent<CameraController>().SetCurrentTarget(gameObjectRival.transform);

//      var systemParticles = GameObject.Find("ParticleSystemCoins");
//      systemParticles.GetComponent<ParticleSystem>().Play();
    }
    
    else if (name == "player")
    {
      Debug.Log("player");
      SetAllSleep(GameObject.Find("person_player"), new[]
      {
        "player_head", "player_body", "player_leg_top", "player_leg_bottom", "player_foot"
      }, RigidbodyConstraints2D.None);
      RevertTimer.TouchEnabled = false;
      var gameObjectRival = GameObject.Find("person_player");
      GameObject.Find("Main Camera").GetComponent<CameraController>().SetCurrentTarget(gameObjectRival.transform);
      GameSession.Get().IsRivalWin = true;
      
      GenerateBlood(other, bullet, true);
      GenerateBloodParticles(other, true);
    }

    Destroy(bullet);

    Debug.Log("collision");
  }

  public void GenerateBloodParticles(Collision2D collision, bool isRevert = false)
  {
    var bloods = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/RivalBloodParticleSystem.prefab"));
    bloods.transform.SetParent(collision.transform);
    bloods.transform.position = collision.transform.position;
    
    if (isRevert)
    {
      bloods.transform.localScale = new Vector3(bloods.transform.localScale.x * -1, bloods.transform.localScale.y, bloods.transform.localScale.z);
    }

    bloods.GetComponent<ParticleSystem>().Play();
  }

  public void GenerateBlood(Collision2D collision, GameObject bullet, bool isRevert = false)
  {
    var blood = new GameObject();
    blood.AddComponent<SpriteRenderer>();
    blood.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Persons/blood_particle.png");
    blood.transform.SetParent(collision.transform, true);
    blood.transform.position = bullet.transform.position;
    blood.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    
    if (isRevert)
    {
      blood.transform.Translate(-collision.gameObject.transform.localScale.x / 2, 0, -3);
    }
    else
    {
      blood.transform.Translate(-collision.gameObject.transform.localScale.x / 2, 0, -3);
    }
  }
  
  private void SetAllSleep(GameObject gObject, string[] names, RigidbodyConstraints2D constraints2D)
  {
    var rbs = gObject.GetComponentsInChildren<Rigidbody2D>().Where(x => names.Contains(x.name)).ToList();
    rbs.ForEach(x => x.constraints = constraints2D);
  }

  void Start()
  {
      RevertTimer = GameObject.Find("RevertTimerText").GetComponent<RevertTimer>();
  }

  void Update()
  {

  }
}
