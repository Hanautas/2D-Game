using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public int followSpeed = 10;

    public Transform target;

    void Update()
    {
        if (Vector3.Distance(transform.position, GetTargetPosition(target)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetTargetPosition(target), Time.deltaTime * followSpeed);
        }
    }

    public void SetSpeed(int newSpeed)
    {
        followSpeed = newSpeed;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public Vector3 GetTargetPosition(Transform target)
    {
        return new Vector3(target.position.x, target.position.y, -10);
    }
}