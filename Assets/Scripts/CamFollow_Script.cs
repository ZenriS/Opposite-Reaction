using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow_Script : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    private Vector3 _vel;
    public float CamShakeSpeed;

    void Start()
    {
        _vel = Vector3.zero;
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (Target != null)
        {
            transform.position = Vector3.SmoothDamp(this.transform.position, Target.position, ref _vel, Speed);
        }
    }

    public void CamShake(float a)
    {
        float xAngle = Random.Range(-a, a);
        float yAngle = Random.Range(-a, a);
        transform.position = transform.position + new Vector3(xAngle, yAngle, 0);
    }
}
