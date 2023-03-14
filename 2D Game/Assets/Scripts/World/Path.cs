using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Path : MonoBehaviour
{
    public bool isAccessible = false;
    public bool isComplete = false;

    public string sceneName = "Combat";

    public Tileset tilesetType;

    public List<Button> buttonList;

    void Start()
    {
        GetButtons();
    }

    private void GetButtons()
    {
        buttonList.Clear();

        foreach (Transform child in transform)
        {
            Button button = child.GetComponent<Button>();

            button.onClick.AddListener(() => SetButtonsInteractable(false));
            button.onClick.AddListener(() => PlayerMovement.instance.SetTargetPosition(child));
            button.onClick.AddListener(() => LoadScene());

            buttonList.Add(button);
        }
    }

    public void IsComplete(bool value)
    {
        isComplete = true;
    }

    public void SetButtonsInteractable(bool value)
    {
        foreach (Button button in buttonList)
        {
            button.interactable = value;
        }
    }

    public void SetTilesetType(Tileset tileset)
    {
        tilesetType = tileset;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneDelay());
    }

    public IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(2f);

        GameManager.instance.LoadSceneAdditive(sceneName);
    }
}