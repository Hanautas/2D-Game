using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    public Transform content;

    private GameObject storeItemPrefab;

    public List<Item> itemList;

    void Start()
    {
        storeItemPrefab = Resources.Load("UI/Store Item") as GameObject;

        GenerateStoreItems();
    }

    private void GenerateStoreItems()
    {
        List<int> numberList = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            int value = Utility.GetRandomValue(0, itemList.Count);

            while (numberList.Contains(value))
            {
                value = Utility.GetRandomValue(0, itemList.Count);
            }

            numberList.Add(value);

            GameObject storeItem = Instantiate(storeItemPrefab, transform.position, Quaternion.identity) as GameObject;

            storeItem.transform.SetParent(content, false);

            Item item = itemList[numberList[i]];

            storeItem.transform.Find("Image/Icon").GetComponent<Image>().sprite = item.itemIcon;

            Transform buttonObject = storeItem.transform.Find("Button");
            buttonObject.transform.Find("Text").GetComponent<TMP_Text>().text = item.itemValue.ToString();

            Button itemButton = buttonObject.GetComponent<Button>();
            itemButton.onClick.AddListener(() => BuyItem());
        }
    }

    public void BuyItem()
    {

    }

    public void SellItem()
    {

    }
}