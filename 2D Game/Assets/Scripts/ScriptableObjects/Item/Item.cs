using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemValue;

    public Sprite itemIcon;

    public virtual void Activate(Unit target)
    {
        Debug.Log("No Function!");
    }
}