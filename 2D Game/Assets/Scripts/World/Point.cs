using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Point : MonoBehaviour
{
    public bool isActivePoint = false;

    public int difficulty = 1;

    public string sceneName = "Combat";

    public Button button;

    public TMP_Text difficultyText;

    private Path parentPath;

    void Update()
    {
        if (isActivePoint)
        {
            if (Vector3.Distance(transform.position, PlayerMovement.instance.GetPosition()) < 0.1f)
            {
                isActivePoint = false;

                StartCoroutine(LoadSceneDelay());
            }
        }
    }

    public void SetPath(Path path)
    {
        parentPath = path;
    }

    public void SetDifficulty(int value)
    {
        difficulty = value;

        difficultyText.text = difficulty.ToString();

        switch (difficulty)
        {
        case 1:
            difficultyText.color = new Color(0f, 1f, 0f);

            break;
        case 2:
            difficultyText.color = new Color(1f, 1f, 0f);

            break;
        case 3:
            difficultyText.color = new Color(1f, 0.5f, 0f);

            break;
        case 4:
            difficultyText.color = new Color(1f, 0f, 0f);

            break;
        default:
            difficultyText.color = new Color(1f, 1f, 1f);

            sceneName = "Town";

            break;
        }
    }

    public void ActivatePoint()
    {
        isActivePoint = true;

        PlayerMovement.instance.SetTargetPosition(transform);
    }

    public void DeactivateButton()
    {
        button.interactable = false;
    }

    public IEnumerator LoadSceneDelay()
    {
        CombatManager.instance.SetDifficulty(difficulty);

        CombatManager.instance.SetTilesetType(parentPath.GetTilesetType());

        yield return new WaitForSeconds(1f);

        GameManager.instance.LoadSceneAdditive(sceneName);
    }

    [ContextMenu("Debug Load Scene")]
    public void DebugLoadScene()
    {
        CombatManager.instance.SetDifficulty(difficulty);

        CombatManager.instance.SetTilesetType(parentPath.GetTilesetType());

        GameManager.instance.LoadSceneAdditive(sceneName);
    }
}