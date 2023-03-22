using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    public bool isActivePoint = false;

    public int difficulty = 1;

    public string sceneName = "Combat";

    public Button button;

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
        yield return new WaitForSeconds(1f);

        GameManager.instance.LoadSceneAdditive(sceneName);
    }
}