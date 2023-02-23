using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private GameObject coinPrefab;

    void Awake()
    {
        coinPrefab = Resources.Load("UI/Coin") as GameObject;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        StartCoroutine(SpawnCoin(10));
    }

    private IEnumerator SpawnCoin(int amount)
    {
        if (amount > 0)
        {
            amount--;

            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity) as GameObject;

            yield return new WaitForSeconds(0.1f);

            StartCoroutine(SpawnCoin(amount));
        }
    }
}