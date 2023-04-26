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

    void Awake()
    {
        storeItemPrefab = Resources.Load("UI/Town/Store Item") as GameObject;
    }

    void Start()
    {
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

            storeItem.transform.Find("Image/Icon").GetComponent<Image>().sprite = item.itemSprite;

            Transform buttonObject = storeItem.transform.Find("Button");
            buttonObject.transform.Find("Text").GetComponent<TMP_Text>().text = item.itemValue.ToString();

            Button itemButton = buttonObject.GetComponent<Button>();
            itemButton.onClick.AddListener(() => BuyItem(item));
        }
    }

    public void BuyItem(Item item)
    {
        if (PlayerData.instance.CheckGold(item.itemValue))
        {
            PlayerData.instance.SetGold(-item.itemValue);
        }

        PlayerData.instance.AddItem(item);
    }

    public void SellItem()
    {

    }
}