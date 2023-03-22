using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public int movementSpeed = 10;

    public Vector3 targetPosition;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition(Utility.GetMousePosition());
        }
        */

        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed);
        }
    }

    public void SetTargetPosition(Transform target)
    {
        targetPosition = target.position;

        FlipX(targetPosition.x);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void FlipX(float xPosition)
    {
        if (xPosition < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}