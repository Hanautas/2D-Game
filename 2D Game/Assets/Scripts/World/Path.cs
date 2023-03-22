using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Path : MonoBehaviour
{
    public bool isAccessible = false;
    public bool isComplete = false;

    public Tileset tilesetType;

    public List<Point> pointList;

    void Start()
    {
        GetPoints();
    }

    private void GetPoints()
    {
        pointList.Clear();

        foreach (Transform child in transform)
        {
            Point point = child.GetComponent<Point>();

            point.button.onClick.AddListener(() => ActivatePoint(point));

            pointList.Add(point);
        }
    }

    public void ActivatePoint(Point point)
    {
        if (isAccessible)
        {
            isAccessible = false;

            point.ActivatePoint();

            foreach (Point p in pointList)
            {
                p.DeactivateButton();
            }
        }
    }

    public void SetComplete()
    {
        isComplete = true;

        WorldManager.instance.NextPath();
    }

    public void SetAccessible()
    {
        isAccessible = true;
    }

    public void SetTilesetType(Tileset tileset)
    {
        tilesetType = tileset;
    }
}