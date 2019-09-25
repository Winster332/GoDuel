using Halper;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform Player;
  public Transform Rival;
  public float Distance;
  public Vector3 Velocity = Vector3.zero;
  public float SmoothTime = .15f;
  
  // camera borders
  public bool IsXLimitEnabled = false;
  public float XMaxValue = 0;
  public float XMinValue = 0;
  public bool IsYLimitEnabled = false;
  public float YMaxValue = 0;
  public float YMinValue = 0;
  public Transform CurrentTarget;
  public GameSchemeTimer SchemeTimer { get; set; }
  public bool UseTimer = true;

  void Start()
  {
//    SetTarget(Rival);
    if (UseTimer)
    {
      SetCurrentTarget(Rival);

      SchemeTimer = new GameSchemeTimer()
        .AddTimer(2, toPlayer => SetCurrentTarget(Player))
        .AddTimer(4, toCenter => SetCurrentTarget(null))
        .StartAll();
    }
  }

  public void SetCurrentTarget(Transform transform)
  {
    CurrentTarget = transform;
  }

  private void SetTarget(Transform gObject)
  {
    transform.position = new Vector3(gObject.transform.position.x, gObject.transform.position.y, Distance);
    transform.LookAt(gObject);
  }

  private void FixedUpdate()
  {
    SchemeTimer?.Update();

    if (CurrentTarget != null)
    {
      UpdateCurrentTarget();
    }
    else
    {
      UpdateCurrentVector(Vector3.zero);
    }
  }

  private void UpdateCurrentVector(Vector3 positionTo)
  {
    var targetPos = positionTo;

    if (IsYLimitEnabled)
    {
      targetPos.y = Mathf.Clamp(positionTo.y, YMinValue, YMaxValue);
    }
    if (IsXLimitEnabled)
    {
      targetPos.x = Mathf.Clamp(positionTo.x, XMinValue, XMaxValue);
    }

    targetPos.z = transform.position.z;

    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref Velocity, SmoothTime);
  }

  private void UpdateCurrentTarget()
  {
    var targetPos = CurrentTarget.position;

    if (IsYLimitEnabled)
    {
      targetPos.y = Mathf.Clamp(CurrentTarget.position.y, YMinValue, YMaxValue);
    }
    if (IsXLimitEnabled)
    {
      targetPos.x = Mathf.Clamp(CurrentTarget.position.x, XMinValue, XMaxValue);
    }

    targetPos.z = transform.position.z;

    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref Velocity, SmoothTime);
  }
}
