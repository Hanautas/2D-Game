using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;

    public GameObject[] menus;

    void Start()
    {
        PlayerData.instance.SetPlayerUIActive(false);
    }

    public void Play()
    {
        PlayerData.instance.SetPlayerUIActive(true);

        GameManager.instance.LoadScene(sceneToLoad);
    }

    public void showMenu(int index)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        menus[index].SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quit");

        Application.Quit();
    }
}