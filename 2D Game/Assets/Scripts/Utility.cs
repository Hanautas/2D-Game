using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    public static int GetRandomValue(int min, int max)
    {
        return Random.Range(min, max);
    }
}