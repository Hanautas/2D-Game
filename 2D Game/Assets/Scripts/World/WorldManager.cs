using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public int pathLength;

    public Transform content;

    private GameObject pathPrefab;
    private GameObject pointPrefab;

    void Awake()
    {
        pathPrefab = Resources.Load("UI/World/Path") as GameObject;
        pointPrefab = Resources.Load("UI/World/Point") as GameObject;
    }

    void Start()
    {
        CreateWorld();
    }

    private void CreateWorld()
    {
        for (int i = 0; i < 2; i++)
        {
            CreatePath(0);
        }

        for (int i = 0; i < pathLength; i++)
        {
            CreatePath(Random.Range(0, 3));
        }

        for (int i = 0; i < 2; i++)
        {
            CreatePath(0);
        }
    }

    public void CreatePath(int pointCount)
    {
        GameObject pathObject = Instantiate(pathPrefab, transform.position, Quaternion.identity) as GameObject;

        pathObject.transform.SetParent(content, false);

        for (int i = 0; i <pointCount; i++)
        {
            GameObject pointObject = Instantiate(pointPrefab, transform.position, Quaternion.identity) as GameObject;

            pointObject.transform.SetParent(pathObject.transform, false);
        }
    }
}