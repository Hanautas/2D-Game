using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> pointList;

    public void GetPoints()
    {
        pointList.Clear();

        foreach (Transform child in transform)
        {
            pointList.Add(child);
        }
    }
}