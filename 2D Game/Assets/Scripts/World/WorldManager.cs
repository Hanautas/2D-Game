using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public int pathLength;

    public Transform content;

    public List<Path> pathList;

    private GameObject pathPrefab;
    private GameObject buttonPrefab;

    void Awake()
    {
        pathPrefab = Resources.Load("UI/World/Path") as GameObject;
        buttonPrefab = Resources.Load("UI/World/Button") as GameObject;
    }

    void Start()
    {
        CreateWorld();
    }

    private void CreateWorld()
    {
        for (int i = 0; i < 2; i++)
        {
            CreatePath(1);
        }

        pathList[0].isAccessible = true;

        for (int i = 0; i < pathLength; i++)
        {
            CreatePath(Random.Range(1, 4));
        }

        for (int i = 0; i < 2; i++)
        {
            CreatePath(1);
        }

        for (int i = 1; i < 4; i++)
        {
            pathList[pathList.Count - i].SetTilesetType(Tileset.Dungeon);
        }
    }

    public void CreatePath(int buttonCount)
    {
        GameObject pathObject = Instantiate(pathPrefab, transform.position, Quaternion.identity) as GameObject;

        pathObject.transform.SetParent(content, false);

        pathList.Add(pathObject.GetComponent<Path>());

        for (int i = 0; i <buttonCount; i++)
        {
            GameObject buttonObject = Instantiate(buttonPrefab, transform.position, Quaternion.identity) as GameObject;

            buttonObject.transform.SetParent(pathObject.transform, false);
        }
    }
}