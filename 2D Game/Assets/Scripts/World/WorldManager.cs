using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    public int pathIndex;

    public int pathLength;

    public Transform content;

    public List<Path> pathList;

    private GameObject pathPrefab;
    private GameObject pointPrefab;

    void Awake()
    {
        instance = this;

        pathPrefab = Resources.Load("UI/World/Path") as GameObject;
        pointPrefab = Resources.Load("UI/World/Point") as GameObject;
    }

    void Start()
    {
        CreateWorld();
    }

    private void CreateWorld()
    {
        // 2 Paths, 1 Point, 1 Difficulty, Range False
        for (int i = 0; i < 2; i++)
        {
            CreatePath(1, 1, false);
        }

        pathList[0].isAccessible = true;

        // {pathLength} Paths, 1-3 Points, 1-4 Difficulty, Range True
        for (int i = 0; i < pathLength; i++)
        {
            CreatePath(Random.Range(1, 4), Random.Range(1, 5), true);
        }

        // 2 Paths, 1 Point, 4 Difficulty, Range False
        for (int i = 0; i < 2; i++)
        {
            CreatePath(1, 4, false);
        }

        for (int i = 1; i < 4; i++)
        {
            pathList[pathList.Count - i].SetTilesetType(Tileset.Dungeon);
        }
    }

    public void CreatePath(int pointCount, int difficulty, bool isRange)
    {
        GameObject pathObject = Instantiate(pathPrefab, transform.position, Quaternion.identity) as GameObject;

        pathObject.transform.SetParent(content, false);

        pathList.Add(pathObject.GetComponent<Path>());

        for (int i = 0; i <pointCount; i++)
        {
            GameObject pointObject = Instantiate(pointPrefab, transform.position, Quaternion.identity) as GameObject;

            pointObject.transform.SetParent(pathObject.transform, false);

            Point point = pointObject.GetComponent<Point>();

            if (isRange)
            {
                point.SetDifficulty(Random.Range(1, difficulty));
            }
            else
            {
                point.SetDifficulty(difficulty);
            }
        }
    }

    public void NextPath()
    {
        pathList[pathIndex].SetComplete();

        if (pathIndex == pathList.Count)
        {
            CompleteWorld();
        }
        else
        {
            pathIndex++;

            pathList[pathIndex].SetAccessible();
        }
    }

    public void CompleteWorld()
    {
        GameManager.instance.LoadScene("Ending");

        GameManager.instance.UnloadScene("World");
    }
}